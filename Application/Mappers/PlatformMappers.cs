using Application.Dtos.Platform;
using Domain.Entities;

namespace Application.Mappers;

public static class PlatformMappers
{
    public static Platform ToAddPlatform(this AddPlatformDto addPlatformDto)
    {
        return new Platform()
        {
            Id = Guid.NewGuid(),
            Type = addPlatformDto.Type,
        };
    }

    public static Platform UpdatePlatform(this Platform platform, UpdatePlatformDto updatePlatformDto)
    {
        platform.Id = updatePlatformDto.Id;
        platform.Type = updatePlatformDto.Type;
        return platform;
    }
}