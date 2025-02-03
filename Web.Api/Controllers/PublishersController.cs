using Application.Dtos.Publisher;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PublishersController : ControllerBase
{
    private readonly IPublisherService _publisherService;

    public PublishersController(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    [HttpGet("{companyName}/games")]
    public async Task<IActionResult> GetGamesByPublisher(string companyName)
    {
        var games = await _publisherService.GetGameByCompanyName(companyName);
        return Ok(games);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPublishers([FromQuery] PublisherFilterDto publisherFilterDto)
    {
        var publishers = await _publisherService.GetAllPublishers(publisherFilterDto);
        return Ok(publishers);
    }
    
    [HttpGet("{companyName}")]
    public async Task<IActionResult> GetPublishersByCompanyName(string companyName)
    {
        var publishers = await _publisherService.GetPublisherByCompanyNameAsync(companyName);
        return Ok(publishers);
    }
    
}