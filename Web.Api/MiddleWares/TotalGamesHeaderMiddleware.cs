using Application.IServices;

namespace Web.Api.MiddleWares;

public class TotalGamesHeaderMiddleware
{
    private readonly RequestDelegate _next;
    
    public TotalGamesHeaderMiddleware(RequestDelegate next) 
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IGameServices gameService)
    {
        var totalGames = await gameService.CountOfGames();

        context.Response.OnStarting(() => 
        {
            context.Response.Headers.Add("x-total-numbers-of-games", totalGames.ToString());
            return Task.CompletedTask;
        });

        await _next(context);
    }
}