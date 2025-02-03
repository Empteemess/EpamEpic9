using Application.Dtos.Comment;
using Application.Dtos.Game;
using Application.Dtos.Publisher;
using Domain.Entities;

namespace Application.IServices;

public interface IGameServices
{
    void DeleteCommentByGameKey(string gameKey, Guid commentId);
    Task<Comment> AddCommentByGameKeyAsync(AddCommentDto addCommentDto, string gameKey);
    Task<int> CountOfGames();
    Task DeleteGameAsync(Guid gameId);
    Task<IEnumerable<GetGameDto>> GetAllGames(GameFilterDto gameFilterDto);
    Task<GetGameDto> GetGameByKeyAsync(string key);
    Task<GetGameDto> GetGameByIdAsync(Guid gameId);
    Task AddGameAsync(GameRequestDto gameRequestDto);
    Task UpdateGameAsync(UpdateGameRequestDto updateGameRequestDto);
    Task<GetPublisherDto> GetPublisherByGameKeyAsync(string gameKey);
    Task<IEnumerable<GetGameDto>> GetGameByGenreIdAsync(Guid genreId);
    Task<IEnumerable<GetGameDto>> GetGameByPlatformIdAsync(Guid platformId);
}