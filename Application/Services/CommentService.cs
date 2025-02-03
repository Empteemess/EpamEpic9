using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BanDurations>> GetAllBanDurations()
    {
        var banDurations = await _unitOfWork.BanDurationRepository.GetAllAsync();
        return banDurations;
    }
}