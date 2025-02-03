using Application.Dtos.Genre;
using Application.Services;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.IRepositories;
using Moq;

namespace GameStore.Tests;

public class GenreServiceTests
{
    private readonly GenreService _sut;
    
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IGenreRepository> _genreRepositoryMock = new();
    
    public GenreServiceTests()
    {
        _sut = new GenreService(_unitOfWorkMock.Object);
        _unitOfWorkMock.Setup(x => x.GenreRepository).Returns(_genreRepositoryMock.Object);
    }
    [Fact]
    public async Task AddGenreAsync_WhenGenreNotExists_AddsGenre()
    {
        //Arrange
        var addgenreDto = new AddGenreDto
        {
            Name = "testName"
        };
        
        _genreRepositoryMock
            .Setup(x => x.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(() => null);
        
        //Act
        await _sut.AddGenreAsync(addgenreDto);
        
        
        //Assert
        
        _genreRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once);
        _genreRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Genre>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task AddGenreAsync_WhenGenreAlreadyExists_ReturnsGenreException()
    {
        //Arrange
        var addgenreDto = new AddGenreDto
        {
            Name = "testName"
        };
        
        var genre = new Genre
        {
            Id = Guid.NewGuid(),
            Name =addgenreDto.Name
        };
        
        _genreRepositoryMock
            .Setup(x => x.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(genre);
        
        //Act
        var exception = await Assert
            .ThrowsAsync<GenreException>(async () => await _sut.AddGenreAsync(addgenreDto));
        
        
        //Assert
        Assert.NotNull(exception);
        Assert.Equal("Genre already exists", exception.Message);
        
        _genreRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task UpdateGenreAsync_WhenGenreValid_UpdatesGenre()
    {
        //Arrange
        var updateGenreResp = new UpdateGenreDto
        {
            Id = Guid.NewGuid(),
            Name = "TestName"
        };

        var genre = new Genre
        {
            Id = updateGenreResp.Id,
            Name = updateGenreResp.Name
        };

        _genreRepositoryMock
            .Setup(x => x.GetByIdAsync(updateGenreResp.Id))
            .ReturnsAsync(genre);
        
        _genreRepositoryMock
            .Setup(x => x.Update(genre))
            .Callback<Genre>(x => x.Id = updateGenreResp.Id);
        
        //Act
        await _sut.UpdateGenreAsync(updateGenreResp);
        
        //Assert
        
        var genreResp = await _genreRepositoryMock.Object.GetByIdAsync(updateGenreResp.Id);
        Assert.NotNull(genreResp);
        
        _genreRepositoryMock
            .Verify(x => x.GetByIdAsync(updateGenreResp.Id), Times.Exactly(2));
        
        _genreRepositoryMock
            .Verify(x => x.Update(genre), Times.Once);
    
        _unitOfWorkMock
            .Verify(x => x.SaveAsync(),Times.Once);
    }
    
    [Fact]
    public async Task UpdateGenreAsync_WhenGenreIsNull_ThrowsNotFoundException()
    {
        //Arrange
        var updateGenreResp = new UpdateGenreDto
        {
            Id = Guid.NewGuid(),
            Name = "TestName"
        };

        _genreRepositoryMock
            .Setup(x => x.GetByIdAsync(updateGenreResp.Id))
            .ReturnsAsync(() => null);

        //Act
        var exception = await Assert
            .ThrowsAsync<GenreException>(async () => await _sut.UpdateGenreAsync(updateGenreResp));
        
        //Assert
        Assert.NotNull(exception);
        Assert.Equal("Genre not found",exception.Message);
        
        _genreRepositoryMock
            .Verify(x => x.GetByIdAsync(updateGenreResp.Id), Times.Once);
    }
}