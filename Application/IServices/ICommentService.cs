using Domain.Entities;

namespace Application.Services;

public interface ICommentService
{
    Task<IEnumerable<BanDurations>> GetAllBanDurations();
}