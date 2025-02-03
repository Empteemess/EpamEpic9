using System.Net;
using Application.Dtos.Comment;
using Application.Dtos.Game;
using Application.Dtos.Publisher;
using Application.IServices;
using Application.Mappers;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using Infrastructure;
using Infrastructure.PipelineSteps;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class GameService : IGameServices
{
    private readonly IUnitOfWork _unitOfWork;

    public GameService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async void DeleteCommentByGameKey(string gameKey, Guid commentId)
    {
        var game = await _unitOfWork.GameRepository.GetByKeyAsync(gameKey);
        if (game is null) throw new GameException("Game not found", (int)HttpStatusCode.NotFound);

        var comment = game.Comments.FirstOrDefault(c => c.Id == commentId);
        if (comment is null) throw new GameException("Comment not found", (int)HttpStatusCode.NotFound);

        _unitOfWork.CommentRepository.DeleteComment(comment);
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByGameKeyAsync(string gameKey)
    {
        var comments = await _unitOfWork.CommentRepository.GetCommentsByGameKeyAsync(gameKey);

        return comments.Select(c => new CommentDto
        {
            Id = c.Id,
            Name = c.Name,
            Body = c.Body,
            ChildComments = c.ChildComments.Select(cc => new CommentDto
            {
                Id = cc.Id,
                Name = cc.Name,
                Body = cc.Body,
                ChildComments = cc.ChildComments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Body = c.Body,
                    ChildComments = new List<CommentDto>()
                }).ToList()
            }).ToList()
        }).ToList();
    }


    public async Task<Comment> AddCommentByGameKeyAsync(AddCommentDto addCommentDto, string gameKey)
    {
        var game = await _unitOfWork.GameRepository.GetByKeyAsync(gameKey);

        var id = Guid.NewGuid();

        var comment = new Comment
        {
            Id = id,
            Name = addCommentDto.Comment.Name,
            Body = addCommentDto.Comment.Body,
            GameId = game.Id,
        };

        if (addCommentDto.ParentId.HasValue)
        {
            comment.ParentCommentId = id;
        }

        await _unitOfWork.CommentRepository.AddComment(comment);
        await _unitOfWork.SaveAsync();

        return comment;
    }

    public async Task<GetPublisherDto> GetPublisherByGameKeyAsync(string gameKey)
    {
        var game = await _unitOfWork.GameRepository.GetByKeyAsync(gameKey);
        if (game is null) throw new GameException("game not found", (int)HttpStatusCode.NotFound);
        if (game.Publisher is null) throw new PublisherException("publisher not found", (int)HttpStatusCode.NotFound);

        return game.Publisher.MapToPublisherDto();
    }

    public async Task<IEnumerable<GetGameDto>> GetAllGames(GameFilterDto gameFilterDto)
    {
        var games = _unitOfWork.GameRepository.GetAllGames();

        var pipeline = new Pipeline<IQueryable<Game>>();

        if (!string.IsNullOrWhiteSpace(gameFilterDto.Name))
        {
            pipeline.AddStep(new GameFilterStep(x => x.Name.Contains(gameFilterDto.Name)));
        }

        if (gameFilterDto.SortingEnum is not null)
        {
            pipeline.AddStep(new GameSortingStep(gameFilterDto.SortingEnum.Value));
        }

        if (gameFilterDto.PaginationEnum is not null && gameFilterDto.PageNumber is not null)
        {
            pipeline.AddStep(new PagingStep(gameFilterDto.PaginationEnum.Value, gameFilterDto.PageNumber.Value));
        }

        var result = pipeline.Execute(games);

        var finalResult = await result.ToListAsync();

        return finalResult.Select(x => x.MapToGetGameDto());
    }

    public async Task<int> CountOfGames()
    {
        var games = await _unitOfWork.GameRepository.CountOfGames();
        return games;
    }

    public async Task UpdateGameAsync(UpdateGameRequestDto updateGameRequestDto)
    {
        var game = await _unitOfWork.GameRepository.GetByIdAsync(updateGameRequestDto.GameId);

        if (game is null) throw new GameException("game Not Found", (int)HttpStatusCode.NotFound);

        await CheckGenreAsync(updateGameRequestDto.Genres);

        await CheckPlatformAsync(updateGameRequestDto.Platforms);

        await CheckPublisherAsync(updateGameRequestDto.PublisherId);

        game.MapToUpdatedGame(updateGameRequestDto);

        _unitOfWork.GameRepository.Update(game);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteGameAsync(Guid gameId)
    {
        var game = await _unitOfWork.GameRepository.GetByIdAsync(gameId);
        if (game is null) throw new GameException("game Not Found", (int)HttpStatusCode.NotFound);

        await _unitOfWork.GameRepository.RemoveAsync(gameId);
        await _unitOfWork.SaveAsync();
    }

    public async Task AddGameAsync(GameRequestDto gameRequestDto)
    {
        await CheckGameByKeyAsync(gameRequestDto);

        await CheckGenreAsync(gameRequestDto.Genres);

        await CheckPlatformAsync(gameRequestDto.Platforms);

        await CheckPublisherAsync(gameRequestDto.Publisher);

        var game = gameRequestDto.MapToGame();

        var filePath = $"{game.Name}_{DateTime.Now}.txt";

        await _unitOfWork.GameRepository.AddAsync(game);
        await _unitOfWork.SaveAsync();
    }

    private async Task CheckPublisherAsync(Guid publisherId)
    {
        var publisher = await _unitOfWork.PublisherRepository.GetPublisherByIdAsync(publisherId);
        if (publisher is null) throw new PublisherException("publisher Not Found", (int)HttpStatusCode.NotFound);
    }

    private async Task CheckPlatformAsync(IEnumerable<Guid> platformIds)
    {
        foreach (var platformId in platformIds)
        {
            var platform = await _unitOfWork.PlatformRepository.GetByIdAsync(platformId);
            if (platform is null) throw new PlatformException("Platform Not Found", (int)HttpStatusCode.NotFound);
        }
    }

    private async Task CheckGenreAsync(IEnumerable<Guid> genres)
    {
        foreach (var genreId in genres)
        {
            var genre = await _unitOfWork.GenreRepository.GetByIdAsync(genreId);
            if (genre is null) throw new GenreException("Genre Not Found", (int)HttpStatusCode.NotFound);
        }
    }

    private async Task CheckGameByKeyAsync(GameRequestDto gameRequestDto)
    {
        var code = Random.Shared.Next(1000, 9999);
        var games = await _unitOfWork.GameRepository.CountOfGames();

        if (games > 0)
        {
            var gamesByKey = await _unitOfWork.GameRepository.GetByKeyAsync(gameRequestDto.Key);

            if (gamesByKey is not null)
                throw new GameException($"Game - {gameRequestDto.Name}- Exists", (int)HttpStatusCode.BadRequest);

            if (gameRequestDto.Key is null)
            {
                var gamesByName = await _unitOfWork.GameRepository.GetByNameAsync(gameRequestDto.Name);

                gameRequestDto.Key = gamesByName.Id != Guid.Empty
                    ? $"{gameRequestDto.Name}{code}"
                    : $"{gameRequestDto.Name}";
            }
        }
    }

    public async Task<GetGameDto> GetGameByKeyAsync(string key)
    {
        var game = await _unitOfWork.GameRepository.GetByKeyAsync(key);
        return game.MapToGetGameDto();
    }

    public async Task<GetGameDto> GetGameByIdAsync(Guid gameId)
    {
        var game = await _unitOfWork.GameRepository.GetByIdAsync(gameId);
        return game.MapToGetGameDto();
    }

    public async Task<IEnumerable<GetGameDto>> GetGameByGenreIdAsync(Guid genreId)
    {
        var games = await _unitOfWork.GameRepository.GetGameByGenreIdAsync(genreId);
        return games.Select(x => x.MapToGetGameDto());
    }

    public async Task<IEnumerable<GetGameDto>> GetGameByPlatformIdAsync(Guid platformId)
    {
        var games = await _unitOfWork.GameRepository.GetGameByPlatformIdAsync(platformId);
        return games.Select(x => x.MapToGetGameDto());
    }
}