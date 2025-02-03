using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PlatformRepository : IPlatformRepository
{
    private readonly DbSet<Platform> _platform;

    public PlatformRepository(AppDbContext context)
    {
        _platform = context.Set<Platform>();
    }

    public async Task AddAsync(Platform platform)
    {
        await _platform.AddAsync(platform);
    }
    
    public async Task<Platform> GetByTypeAsync(string type)
    {
        var platform = await _platform.FirstOrDefaultAsync(i => i.Type == type);
        return platform;
    }
    
    public async Task<Platform> GetByIdAsync(Guid id)
    {
        var platform = await _platform.FirstOrDefaultAsync(i => i.Id == id);
        return platform;
    }

    public async Task<IEnumerable<Platform>> GetAllAsync()
    {
        var platforms = await _platform.ToListAsync();
        return platforms;
    }

    public async Task<Platform> GetPlatformByGameKeyAsync(string gameKey)
    {
        var platform = await _platform
            .AsNoTracking()
            .Include(gp => gp.GamePlatforms)
            .ThenInclude(g => g.Game)
            .FirstOrDefaultAsync(gp => gp.GamePlatforms.Any(gk => gk.Game.Key == gameKey));

        return platform;
    }

    public void Update(Platform platform)
    {
        _platform.Update(platform);
    }

    public async Task DeleteAsync(Guid id)
    {
        var platform = await _platform.FirstOrDefaultAsync(i => i.Id == id);
        _platform.Remove(platform);
    }
}