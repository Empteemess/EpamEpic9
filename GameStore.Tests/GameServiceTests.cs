using Application.Dtos.Game;
using Application.IServices;
using Application.Services;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.IRepositories;
using Moq;

namespace GameStore.Tests;

public class GameServiceTests
{
    private readonly IGameServices _sut;

    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IGameRepository> _gameRepositoryMock = new();
    private readonly Mock<IGenreRepository> _genreRepositoryMock = new();
    private readonly Mock<IPlatformRepository> _platformRepositoryMock = new();

    public GameServiceTests()
    {
        _sut = new GameService(_unitOfWorkMock.Object);

        _unitOfWorkMock.Setup(x => x.GameRepository).Returns(_gameRepositoryMock.Object);
        _unitOfWorkMock.Setup(x => x.GenreRepository).Returns(_genreRepositoryMock.Object);
        _unitOfWorkMock.Setup(x => x.PlatformRepository).Returns(_platformRepositoryMock.Object);
    }
   
    [Theory]
    [InlineData("GameTestKey","GameTestName")]
    [InlineData("GameTestKey1","GameTestName1")]
    [InlineData("GameTestKey2","GameTestName2")]
    public async Task AddGameAsync_WhenPlatformNotFound_ReturnsGameException(string gameKey, string gameName)
    {
        //Arrange
        var gameRequestDto = new GameRequestDto
        {
            Name = gameName,
            Genres = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            },
            Platforms = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            },
            Price = 0,
            UnitInStock = 0,
            Discount = 0,
            Publisher = default
        };

        var game = new Game
        {
            Id = Guid.NewGuid(),
            Name = gameName,
            Key = gameKey,
            Price = 0,
            UnitInStock = 0,
            Discount = 0,
            PublisherId = default
        };

        var genre = new Genre
        {
            Name = "testGenre"
        };

        _gameRepositoryMock
            .Setup(x => x.CountOfGames())
            .ReturnsAsync(3);

        _gameRepositoryMock
            .Setup(x => x.GetByNameAsync(gameRequestDto.Name))
            .ReturnsAsync(game);
        
        _genreRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(genre);
        
        _platformRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);
        
        
        //Act
        var exception = await Assert
            .ThrowsAsync<PlatformException>(async () => await _sut.AddGameAsync(gameRequestDto));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("Platform Not Found", exception.Message);
        

        //Assert
        _gameRepositoryMock.Verify(x => x.CountOfGames(), Times.Once);
        _gameRepositoryMock.Verify(x => x.GetByKeyAsync(It.IsAny<string>()), Times.Once);
        _gameRepositoryMock.Verify(x => x.GetByNameAsync(gameRequestDto.Name), Times.Once);
        
        _platformRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }
    
    [Theory]
    [InlineData("GameTestKey","GameTestName")]
    [InlineData("GameTestKey1","GameTestName1")]
    [InlineData("GameTestKey2","GameTestName2")]
    public async Task AddGameAsync_WhenGenreNotFound_ReturnsGameException(string gameKey, string gameName)
    {
        //Arrange
        var gameRequestDto = new GameRequestDto
        {
            Name = gameName,
            Genres = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            },
            Platforms = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            },
            Price = 0,
            UnitInStock = 0,
            Discount = 0,
            Publisher = default
        };

        var game = new Game
        {
            Id = Guid.NewGuid(),
            Name = gameName,
            Key = gameKey,
            Price = 0,
            UnitInStock = 0,
            Discount = 0,
            PublisherId = default
        };

        _gameRepositoryMock
            .Setup(x => x.CountOfGames())
            .ReturnsAsync(3);

        _gameRepositoryMock
            .Setup(x => x.GetByNameAsync(gameRequestDto.Name))
            .ReturnsAsync(game);
        
        _genreRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);
        
        //Act
         var exception = await Assert
         .ThrowsAsync<GenreException>(async () => await _sut.AddGameAsync(gameRequestDto));

        // Assert
         Assert.NotNull(exception);
         Assert.Equal("Genre Not Found", exception.Message);
        

        //Assert
        _gameRepositoryMock.Verify(x => x.CountOfGames(), Times.Once);
        _gameRepositoryMock.Verify(x => x.GetByKeyAsync(It.IsAny<string>()), Times.Once);
        _gameRepositoryMock.Verify(x => x.GetByNameAsync(gameRequestDto.Name), Times.Once);
    }

    [Theory]
    [InlineData("GameTestKey", "GameTestName")]
    [InlineData("GameTestKey1", "GameTestName1")]
    [InlineData("GameTestKey2", "GameTestName2")]
    public async Task AddGameAsync_WhenGameWithGivenKeyExists_ReturnsNotFoundException(string gameKey, string gameName)
    {
        //Arrange
        var gameRequestDto = new GameRequestDto
        {
            Name = gameName,
            Key = gameKey,
            Genres = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            },
            Platforms = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            },
            Price = 0,
            UnitInStock = 0,
            Discount = 0,
            Publisher = default
        };

        var game = new Game
        {
            Name = gameName,
            Key = gameKey,
            Price = 0,
            UnitInStock = 0,
            Discount = 0,
            PublisherId = default
        };

        _gameRepositoryMock
            .Setup(x => x.CountOfGames())
            .ReturnsAsync(3);

        _gameRepositoryMock
            .Setup(x => x.GetByKeyAsync(gameRequestDto.Key))
            .ReturnsAsync(game);

        //Act
        var exception = await Assert
            .ThrowsAsync<GameException>(async () => await _sut.AddGameAsync(gameRequestDto));

        //Assert
        Assert.NotNull(exception);
        Assert.Equal($"Game - {gameRequestDto.Name}- Exists", exception.Message);

        _gameRepositoryMock.Verify(x => x.CountOfGames(), Times.Once);
        _gameRepositoryMock.Verify(x => x.GetByKeyAsync(gameRequestDto.Key), Times.Once);
    }

    [Fact]
    public async Task DeleteGameAsync_WhenGameExists_RemovesGame()
    {
        //Arrange
        var gameId = Guid.NewGuid();
        var game = new Game
        {
            Name = "TestName",
            Key = "TestKey",
            Price = 0,
            UnitInStock = 0,
            Discount = 0,
            PublisherId = default,
        };

        _gameRepositoryMock
            .Setup(x => x.GetByIdAsync(gameId))
            .ReturnsAsync(game);

        _gameRepositoryMock
            .Setup(x => x.RemoveAsync(gameId))
            .Returns(Task.CompletedTask);

        //Act
        await _sut.DeleteGameAsync(gameId);

        //Assert
        var result = await _gameRepositoryMock.Object.GetByIdAsync(gameId);
        Assert.NotNull(result);
        
        _gameRepositoryMock.Verify(x => x.RemoveAsync(gameId), Times.Once);
        _gameRepositoryMock.Verify(x => x.GetByIdAsync(gameId), Times.Exactly(2));
    }

    [Fact]
    public async Task DeleteGameAsync_WhenGameNotExists_ShouldThrowNotFoundException()
    {
        //Arrange
        var userId = Guid.NewGuid();

        _gameRepositoryMock
            .Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(() => null);

        //Act
        var exception = await Assert
            .ThrowsAsync<GameException>(() => _sut.DeleteGameAsync(userId));

        //Assert
        Assert.NotNull(exception);
        Assert.Equal("game Not Found", exception.Message);

        _gameRepositoryMock.Verify(x => x.GetByIdAsync(userId), Times.Once);
    }

    [Fact]
    public async Task UpdateGameAsync_WhenGameExists_UpdatesGame()
    {
        //Arrange
        var updateDto = new UpdateGameRequestDto
        {
            GameId = Guid.NewGuid(),
            Name = "test",
            Genres = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            },
            Platforms = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            },
            Price = 0,
            UnitInStock = 0,
            Discount = 0,
            PublisherId = default
        };

        var game = new Game
        {
            Name = updateDto.Name,
            Key = updateDto.Name,
            Price = 0,
            UnitInStock = 0,
            Discount = 0,
            PublisherId = default,
        };


        _gameRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(game);
        
        

        //Act
        var exception = await Assert
            .ThrowsAsync<GenreException>(async () => await _sut.UpdateGameAsync(updateDto));

        //Assert
        Assert.Equal("Genre Not Found", exception.Message);
        _gameRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);

        var gameException = await _gameRepositoryMock.Object.GetByIdAsync(It.IsAny<Guid>());
        Assert.NotNull(gameException);

    }

    [Fact]
    public async Task UpdateGameAsync_WhenGameIsNull_ThrowsNotFoundException()
    {
        //Arrange
        var updateDto = new UpdateGameRequestDto
        {
            Name = "test",
            Genres = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            },
            Platforms = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            },
            Price = 0,
            UnitInStock = 0,
            Discount = 0,
            PublisherId = default
        };

        _gameRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        //Act
        var exception = await Assert
            .ThrowsAsync<GameException>(async () => await _sut.UpdateGameAsync(updateDto));

        //Assert
        _gameRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);

        var gameException = await _gameRepositoryMock.Object.GetByIdAsync(It.IsAny<Guid>());
        Assert.Null(gameException);
        Assert.Equal("game Not Found", exception.Message);
    }
}