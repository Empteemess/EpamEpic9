using Application.Dtos.Genre;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGameServices _gameServices;
    private readonly IGenreService _genreService;

    public GenresController(IGameServices gameServices,
        IGenreService genreService)
    {
        _gameServices = gameServices;
        _genreService = genreService;
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteGenre([FromRoute] Guid id)
    {
        await _gameServices.DeleteGameAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreDto updateGenreDto)
    {
        await _genreService.UpdateGenreAsync(updateGenreDto);
        return Ok("update genre successfully");
    }

    [HttpGet("{id}/genres")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetGenresByParentId(Guid id)
    {
        var genre = await _genreService.GetGenreByParentIdAsync(id);
        return Ok(genre);
    }

    [HttpGet("{genreId:guid}/games")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetGameByGenre(Guid genreId)
    {
        var games = await _gameServices.GetGameByGenreIdAsync(genreId);
        return Ok(games);
    }

    [HttpPost]
    public async Task<IActionResult> AddGenre([FromBody] AddGenreDto addGenreDto)
    {
        await _genreService.AddGenreAsync(addGenreDto);
        return Ok("Genre added");
    }

    [HttpGet("{id:guid}")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetGenreById([FromRoute] Guid id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);
        return Ok(genre);
    }

    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _genreService.GetAllAsync();
        return Ok(genres);
    }
}