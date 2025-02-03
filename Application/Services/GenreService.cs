using System.Net;
using Application.Dtos.Genre;
using Application.IServices;
using Application.Mappers;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

public class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;

    public GenreService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddGenreAsync(AddGenreDto addGenreDto)
    {
        var genreByName = await _unitOfWork.GenreRepository.GetByNameAsync(addGenreDto.Name);
        if (genreByName is not null)
            throw new GenreException("Genre already exists", (int)HttpStatusCode.BadRequest);
        
        await _unitOfWork.GenreRepository.AddAsync(addGenreDto.ToAddGenre());
        await _unitOfWork.SaveAsync();
    }

    public async Task<Genre> GetGenreByIdAsync(Guid genreId)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(genreId);
        return genre;
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        var genres = await _unitOfWork.GenreRepository.GetAllAsync();
        return genres;
    }

    public async Task<Genre> GetGenreByGameKeyAsync(string gameKey)
    {
        var genre = await _unitOfWork.GenreRepository.GetGenreByGameKeyAsync(gameKey);
        return genre;
    }

    public async Task<Genre> GetGenreByParentIdAsync(Guid parentId)
    {
        var genre = await _unitOfWork.GenreRepository.GetGenreByParentIdAsync(parentId);
        return genre;
    }

    public async Task UpdateGenreAsync(UpdateGenreDto updateGenreDto)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(updateGenreDto.Id);
        if(genre is null) throw new GenreException("Genre not found",(int)HttpStatusCode.NotFound);

        genre.ToUpdatedGenre(updateGenreDto);
        _unitOfWork.GenreRepository.Update(genre);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteGenreAsync(Guid genreId)
    {
        await _unitOfWork.GenreRepository.DeleteAsync(genreId);
        await _unitOfWork.SaveAsync();
    }
}