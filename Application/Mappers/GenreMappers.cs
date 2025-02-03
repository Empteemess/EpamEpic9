using Application.Dtos.Genre;
using Domain.Entities;

namespace Application.Mappers;

public static class GenreMappers
{
    public static Genre ToAddGenre(this AddGenreDto genreDto)
    {
        return new Genre()
        {
            Id = Guid.NewGuid(),
            Name = genreDto.Name,
            ParentGenreId = genreDto.ParentGenreId
        };
    }
    
    public static Genre ToUpdatedGenre(this Genre genre,UpdateGenreDto updateGenreDto)
    {
        genre.Id = updateGenreDto.Id;
        genre.Name = updateGenreDto.Name;
        genre.ParentGenreId = updateGenreDto.ParentGenreId;
        
        return genre;
    }
}