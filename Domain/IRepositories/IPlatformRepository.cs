using Domain.Entities;

namespace Domain.IRepositories;

public interface IPlatformRepository
{
    Task<Platform> GetByTypeAsync(string type);
    Task AddAsync(Platform platform);
    Task<Platform> GetByIdAsync(Guid id);
    Task<IEnumerable<Platform>> GetAllAsync();
    Task<Platform> GetPlatformByGameKeyAsync(string gameKey);
    void Update(Platform platform);
    Task DeleteAsync(Guid id);
}