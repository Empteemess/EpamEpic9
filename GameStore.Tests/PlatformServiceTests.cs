using Application.Dtos.Platform;
using Application.IServices;
using Application.Mappers;
using Application.Services;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.IRepositories;
using Moq;

namespace GameStore.Tests;

public class PlatformServiceTests
{
    private readonly IPlatformService _sut;

    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IPlatformRepository> _platformRepositoryMock = new();

    public PlatformServiceTests()
    {
        _sut = new PlatformService(_unitOfWorkMock.Object);

        _unitOfWorkMock
            .Setup(x => x.PlatformRepository)
            .Returns(_platformRepositoryMock.Object);
    }

    [Fact]
    public async Task AddPlatformAsync_WhenPlatformNotExists_AddsNewPlatform()
    {
        //Arrange
        var addgenreDto = new AddPlatformDto()
        {
            Type = "testPlatform"
        };

        _platformRepositoryMock
            .Setup(x => x.GetByTypeAsync(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        //Act
        await _sut.AddPlatformAsync(addgenreDto);

        //Assert
        _platformRepositoryMock.Verify(x => x.GetByTypeAsync(It.IsAny<string>()), Times.Once);
        _platformRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Platform>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task AddPlatformAsync_WhenPlatformAlreadyExists_ReturnsPlatformException()
    {
        //Arrange
        var addgenreDto = new AddPlatformDto()
        {
            Type = "testPlatform"
        };

        var platform = new Platform()
        {
            Id = Guid.NewGuid(),
            Type = addgenreDto.Type
        };

        _platformRepositoryMock
            .Setup(x => x.GetByTypeAsync(It.IsAny<string>()))
            .ReturnsAsync(platform);

        //Act
        var exception = await Assert
            .ThrowsAsync<PlatformException>(async () => await _sut.AddPlatformAsync(addgenreDto));


        //Assert
        Assert.NotNull(exception);
        Assert.Equal($"Platform {addgenreDto.Type} already exists", exception.Message);

        _platformRepositoryMock.Verify(x => x.GetByTypeAsync(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task UpdatePlatformAsync_WhenPlatformExists_UpdatesPlatform()
    {
        //Arrange
        var updatePlatform = new UpdatePlatformDto
        {
            Id = Guid.NewGuid(),
            Type = "TestType"
        };

        var platform = new Platform
        {
            Type = "TestType",
            Id = updatePlatform.Id,
        };

        _platformRepositoryMock
            .Setup(x => x.GetByIdAsync(updatePlatform.Id))
            .ReturnsAsync(platform);

        _platformRepositoryMock
            .Setup(x => x.Update(platform))
            .Callback<Platform>(x => x.Id = updatePlatform.Id);

        //Act
        await _sut.UpdatePlatformAsync(updatePlatform);

        //Assert
        var platformExc = await _platformRepositoryMock.Object.GetByIdAsync(updatePlatform.Id);
        Assert.NotNull(platformExc);

        _platformRepositoryMock
            .Verify(x => x.GetByIdAsync(updatePlatform.Id), Times.Exactly(2));

        _platformRepositoryMock
            .Verify(x => x.Update(platform), Times.Once);

        _unitOfWorkMock
            .Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdatePlatformAsync_WhenPlatformsAreNull_ThrowsNotFoundException()
    {
        //Arrange
        var updatePlatform = new UpdatePlatformDto
        {
            Id = Guid.NewGuid(),
            Type = "TestType"
        };

        _platformRepositoryMock
            .Setup(x => x.GetByIdAsync(updatePlatform.Id))
            .ReturnsAsync(() => null);

        //Act
        var exception = await Assert
            .ThrowsAsync<PlatformException>(async () => await _sut.UpdatePlatformAsync(updatePlatform));

        //Assert
        var platform = await _platformRepositoryMock.Object.GetByIdAsync(updatePlatform.Id);
        Assert.Null(platform);

        Assert.Equal("platform not found", exception.Message);

        _platformRepositoryMock
            .Verify(x => x.GetByIdAsync(updatePlatform.Id), Times.Exactly(2));

        _unitOfWorkMock
            .Verify(x => x.SaveAsync(), Times.Never);
    }
}