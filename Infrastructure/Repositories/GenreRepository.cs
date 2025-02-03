using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly DbSet<Genre> _genre;

    public GenreRepository(AppDbContext context)
    {
        _genre = context.Set<Genre>();
    }

    public async Task AddAsync(Genre genre)
    {
        await _genre.AddAsync(genre);
    }
    
    public async Task<Genre> GetByNameAsync(string name)
    {
        var genre = await _genre.FirstOrDefaultAsync(i => i.Name == name);
        return genre;
    }
    
    public async Task<Genre> GetByIdAsync(Guid id)
    {
        var genre = await _genre.FirstOrDefaultAsync(i => i.Id == id);
        return genre;
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        var genres = await _genre.ToListAsync();
        return genres;
    }

    public async Task<Genre> GetGenreByGameKeyAsync(string gameKey)
    {
        var genre = await _genre
            .AsNoTracking()
            .Include(gg => gg.GameGenres)
            .ThenInclude(g => g.Game)
            .FirstOrDefaultAsync(gk => gk.GameGenres.Any(g => g.Game.Key == gameKey));

        return genre;
    }

    public async Task<Genre> GetGenreByParentIdAsync(Guid parentId)
    {
        var genre = await _genre.FirstOrDefaultAsync(pgi => pgi.ParentGenreId == parentId);
        return genre;
    }

    public void Update(Genre genre)
    {
         _genre.Update(genre);
    }

    public async Task DeleteAsync(Guid genreId)
    {
        var genre = await _genre.FirstOrDefaultAsync(g => g.Id == genreId);
        _genre.Remove(genre);
    }
}