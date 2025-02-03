using Domain.Entities;

namespace Domain.IRepositories;

public interface IGenreRepository
{
    Task<Genre> GetByNameAsync(string name);
    Task AddAsync(Genre genre);
    Task<Genre> GetByIdAsync(Guid id);
    Task<IEnumerable<Genre>> GetAllAsync();
    Task<Genre> GetGenreByGameKeyAsync(string gameKey);
    Task<Genre> GetGenreByParentIdAsync(Guid parentId);
    void Update(Genre genre);
    Task DeleteAsync(Guid genreId);
}