using Domain.Entities;

namespace Domain.IRepositories;

public interface IGameRepository
{
    Task<int> CountOfGames();
    IQueryable<Game> GetAllGames();
    Task<IEnumerable<Game>> GetDetailedGamesAsync();
    Task<IEnumerable<Game>> GetGameByPlatformIdAsync(Guid platformId);
    Task<IEnumerable<Game>> GetGameByGenreIdAsync(Guid genreId);
    void Update(Game game);
    Task RemoveAsync(Guid gameId);
    Task AddAsync(Game game);
    Task<Game> GetByIdAsync(Guid gameId);
    Task<Game> GetByKeyAsync(string key);
    Task<Game> GetByNameAsync(string name);
}