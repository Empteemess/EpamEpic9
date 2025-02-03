using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly DbSet<Game> _game;

    public GameRepository(AppDbContext context)
    {
        _game = context.Set<Game>();
    }

    public IQueryable<Game> GetAllGames()
    {
        var games = _game.AsQueryable();

        return games;
    }

    //TODO:What can I do instead of !
    public async Task<IEnumerable<Game>> GetDetailedGamesAsync()
    {
        var games = await _game
            .AsNoTracking()
            .Include(gp => gp.GamePlatforms)!
            .ThenInclude(p => p.Platform)
            .Include(gg => gg.GameGenres)!
            .ThenInclude(g => g.Genre)
            .ToListAsync();

        return games;
    }

    public async Task<int> CountOfGames()
    {
        var games = await _game.CountAsync();
        return games;
    }

    public async Task<IEnumerable<Game>> GetGameByPlatformIdAsync(Guid platformId)
    {
        var games = await _game
            .AsNoTracking()
            .Include(gg => gg.GamePlatforms)!
            .ThenInclude(g => g.Platform)
            .Where(gg => gg.GamePlatforms.Any(gn => gn.Platform.Id == platformId))
            .ToListAsync();

        return games;
    }

    public async Task<IEnumerable<Game>> GetGameByGenreIdAsync(Guid genreId)
    {
        var games = await _game
            .AsNoTracking()
            .Include(gg => gg.GameGenres)!
            .ThenInclude(g => g.Genre)
            .Where(g => g.GameGenres.Any(gn => gn.Genre.Id == genreId))
            .ToListAsync();

        return games;
    }

    public void Update(Game game)
    {
        _game.Update(game);
    }

    public async Task RemoveAsync(Guid gameId)
    {
        var currentGame = await _game.Where(gi => gi.Id == gameId).FirstAsync();
        _game.Remove(currentGame);
    }

    public async Task AddAsync(Game game)
    {
        await _game.AddAsync(game);
    }

    public async Task<Game> GetByIdAsync(Guid gameId)
    {
        var game = await _game.Where(gi => gi.Id == gameId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return game;
    }

    public async Task<Game> GetByKeyAsync(string key)
    {
        var game = await _game
            .Where(k => k.Key == key)
            .Include(x => x.Publisher)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return game;
    }

    public async Task<Game> GetByNameAsync(string name)
    {
        var game = await _game.FirstOrDefaultAsync(x => x.Name == name);
        return game;
    }
}