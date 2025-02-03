using Application.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpDelete("ban/durations")]
    public async Task<IActionResult> GetAllDurations()
    {
        var durations = await _commentService.GetAllBanDurations();

        return Ok(durations);
    }
}