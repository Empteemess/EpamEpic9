using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DbSet<Comment> _comment;

    public CommentRepository(AppDbContext context)
    {
        _comment = context.Set<Comment>();
    }

    public async Task AddComment(Comment comment)
    {
        await _comment.AddAsync(comment);
    }

    public void DeleteComment(Comment comment)
    {
        _comment.Remove(comment);
    }

    public async Task<IEnumerable<Comment>> GetCommentsByGameKeyAsync(string gameKey)
    {
        return await _comment
            .Include(c => c.ChildComments)
            .Where(c => c.GameId == _comment.FirstOrDefault(g => g.Game.Key == gameKey).Id)
            .ToListAsync();
    }
}