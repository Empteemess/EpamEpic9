using Application.Dtos.Platform;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IGameServices _gameServices;
    private readonly IPlatformService _platformService;

    public PlatformsController(IGameServices gameServices,
        IPlatformService platformService)
    {
        _gameServices = gameServices;
        _platformService = platformService;
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePlatform(Guid id)
    {
        await _platformService.DeletePlatformAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePlatform([FromBody] UpdatePlatformDto updatePlatformDto)
    {
        await _platformService.UpdatePlatformAsync(updatePlatformDto);
        return Ok();
    }

    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetAllPlatforms()
    {
        var platforms = await _platformService.GetAllPlatformsAsync();
        return Ok(platforms);
    }

    [HttpGet("{id:guid}")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetPlatformById(Guid id)
    {
        var platform = await _platformService.GetPlatformByIdAsync(id);
        return Ok(platform);
    }

    [HttpPost]
    public async Task<IActionResult> AddPlatform([FromBody] AddPlatformDto addPlatformDto)
    {
        await _platformService.AddPlatformAsync(addPlatformDto);
        return Ok();
    }

    [HttpGet("{id:guid}/games")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetGameByPlatformId(Guid id)
    {
        var games = await _gameServices.GetGameByPlatformIdAsync(id);
        return Ok(games);
    }
}