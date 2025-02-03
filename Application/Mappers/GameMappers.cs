using Application.Dtos.Game;
using Domain.Entities;

namespace Application.Mappers;

public static class GameMappers
{
    public static GetGameDto MapToGetGameDto(this Game game)
    {
        var gameDto = new GetGameDto
        {
            Id = game.Id,
            Name = game.Name,
            Key = game.Key,
            Price = game.Price,
            UnitInStock = game.UnitInStock,
            Discount = game.Discount,
        };
        
        return gameDto;
    }
    
    public static Game MapToGame(this GameRequestDto gameRequestDto)
    {
        var gameId = Guid.NewGuid();
        
        return new Game
        {
            Id = gameId,
            Name = gameRequestDto.Name,
            Description = gameRequestDto.Description,
            Key = gameRequestDto.Key,
            GameGenres = gameRequestDto.Genres.Select(id => new GameGenre
                {
                    Id = Guid.NewGuid(),
                    GameId = gameId,
                    GenreId = id
                })
                .ToList(),
            GamePlatforms = gameRequestDto.Platforms.Select(id => new GamePlatform()
                {
                    Id = Guid.NewGuid(),
                    GameId = gameId,
                    PlatformId = id
                })
                .ToList(),
            Price = gameRequestDto.Price,
            UnitInStock = gameRequestDto.UnitInStock,
            PublisherId = gameRequestDto.Publisher,
            Discount = gameRequestDto.Discount,
        };
    }
    
    public static Game MapToUpdatedGame(this Game game,UpdateGameRequestDto updateGameRequestDto)
    {

        game.Id = updateGameRequestDto.GameId;
        game.Name = updateGameRequestDto.Name;
        game.Description = updateGameRequestDto.Description;
        game.Key = updateGameRequestDto.Key ?? updateGameRequestDto.Name;
        game.GameGenres = updateGameRequestDto.Genres.Select(id => new GameGenre
        {
            Id = Guid.NewGuid(),
            GameId = updateGameRequestDto.GameId,
            GenreId = id
        }).ToList();
        game.GamePlatforms = updateGameRequestDto.Platforms.Select(id => new GamePlatform()
        {
            Id = Guid.NewGuid(),
            GameId = updateGameRequestDto.GameId,
            PlatformId = id
        }).ToList();
        
        game.Price = updateGameRequestDto.Price;
        game.UnitInStock = updateGameRequestDto.UnitInStock;
        game.PublisherId = updateGameRequestDto.PublisherId;
        game.Discount = updateGameRequestDto.Discount;
        
        return game;
    }
}