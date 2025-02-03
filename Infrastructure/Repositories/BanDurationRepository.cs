using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BanDurationRepository : IBanDurationRepository
{
    private readonly DbSet<BanDurations> _durations;

    public BanDurationRepository(AppDbContext context)
    {
        _durations = context.Set<BanDurations>();
    }

    public async Task<IEnumerable<BanDurations>> GetAllAsync()
    {
        return await _durations.ToListAsync();
    }
}