using Application.Dtos.Platform;
using Domain.Entities;

namespace Application.IServices;

public interface IPlatformService
{
    Task AddPlatformAsync(AddPlatformDto addPlatformDto);
    Task<Platform> GetPlatformByIdAsync(Guid platformId);
    Task<IEnumerable<Platform>> GetAllPlatformsAsync();
    Task<Platform> GetPlatformByGameKeyAsync(string gameKey);
    Task UpdatePlatformAsync(UpdatePlatformDto updatePlatformDto);
    Task DeletePlatformAsync(Guid platformId);
}