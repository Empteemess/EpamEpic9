using Application.Dtos.Genre;
using Domain.Entities;

namespace Application.IServices;

public interface IGenreService
{
    Task AddGenreAsync(AddGenreDto addGenreDto);
    Task<Genre> GetGenreByIdAsync(Guid genreId);
    Task<IEnumerable<Genre>> GetAllAsync();
    Task<Genre> GetGenreByGameKeyAsync(string gameKey);
    Task<Genre> GetGenreByParentIdAsync(Guid parentId);
    Task UpdateGenreAsync(UpdateGenreDto updateGenreDto);
    Task DeleteGenreAsync(Guid genreId);
}