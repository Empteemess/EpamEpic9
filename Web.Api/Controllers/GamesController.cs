using Application.Dtos.Comment;
using Application.Dtos.Game;
using Application.Dtos.Publisher;
using Application.IServices;
using Domain.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/games")]
public class GamesController : ControllerBase
{
    private readonly IGameServices _gameServices;
    private readonly IGenreService _genreService;
    private readonly IPlatformService _platformService;
    private readonly IPublisherService _publisherService;
    private readonly IOrderService _orderService;

    public GamesController(IGameServices gameServices,
        IGenreService genreService,
        IPlatformService platformService,
        IPublisherService publisherService, IOrderService orderService)
    {
        _gameServices = gameServices;
        _genreService = genreService;
        _platformService = platformService;
        _publisherService = publisherService;
        _orderService = orderService;
    }

    [HttpDelete("{key}/comments/{id:guid}")]
    public async Task<IActionResult> DeleteComment(string key, Guid id)
    {
        _gameServices.DeleteCommentByGameKey(key, id);
        return Ok();
    }
    
    [HttpPost("{key}/comments")]
    public async Task<IActionResult> AddComment(string key, [FromBody] AddCommentDto addCommentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var comment = await _gameServices.AddCommentByGameKeyAsync(addCommentDto, key);
        return Ok(comment);
    }
    
    [HttpPost("{key}/buy")]
    public async Task<IActionResult> BuyGame(string key)
    {
        await _orderService.AddGameInCartAsync(key);
        return NoContent();
    }
    
    [HttpPut("publisher")]
    public async Task<IActionResult> UpdatePublisher(UpdatePublisherDto updatePublisherDto)
    {
        await _publisherService.UpdatePublisher(updatePublisherDto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisher(Guid id)
    {
        await _publisherService.DeletePublisher(id);
        return NoContent();
    }
    
    [HttpPost("publisher")]
    public async Task<IActionResult> AddPublisher(PublisherRequestDto publisherRequestDto)
    { 
        await _publisherService.CreatePublisherAsync(publisherRequestDto);
        return NoContent();
    }
    
    [HttpGet("{key}/publisher")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetPublisherByGameKey(string key)
    {
        var publisher = await _publisherService.GetPublisherByGameKeyAsync(key);
        return Ok(publisher);
    }
    
    [HttpGet("{key}/platforms")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetPlatformsByGameKey(string key)
    {
        var platforms = await _platformService.GetPlatformByGameKeyAsync(key);
        return Ok(platforms);
    }

    [HttpGet("{key}/genres")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetGenresByGameKey(string key)
    {
        var genres = await _genreService.GetGenreByGameKeyAsync(key);
        return Ok(genres);
    }

    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetAllGames([FromQuery] GameFilterDto gameFilterDto)
    {
        var games = await _gameServices.GetAllGames(gameFilterDto);
        return Ok(games);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGame(UpdateGameRequestDto updateGameRequestDto)
    {
        await _gameServices.UpdateGameAsync(updateGameRequestDto);
        return Ok("Game updated");
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] GameRequestDto gameRequestDto)
    {
        await _gameServices.AddGameAsync(gameRequestDto);
        return Ok("Game added");
    }

    [HttpGet("{key}")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetGameByKey(string key)
    {
        var games = await _gameServices.GetGameByKeyAsync(key);
        return Ok(games);
    }

    [HttpGet("find/{gameId:guid}")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetGameById(Guid gameId)
    {
        var games = await _gameServices.GetGameByIdAsync(gameId);
        return Ok(games);
    } 
    
    //TODO:KEY?
    [HttpDelete("{key:guid}")]
    public async Task<IActionResult> DeleteGameById(Guid key)
    {
        await _gameServices.DeleteGameAsync(key);
        return Ok("Removed Successfully");
    }
}