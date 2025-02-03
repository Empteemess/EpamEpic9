using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IBanDurationRepository
{
    Task<IEnumerable<BanDurations>> GetAllAsync();
}