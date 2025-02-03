using Domain.Entities;

namespace Infrastructure.Repositories;

public interface ICommentRepository
{
    void DeleteComment(Comment comment);
    Task<IEnumerable<Comment>> GetCommentsByGameKeyAsync(string gameKey);
    Task AddComment(Comment comment);
}