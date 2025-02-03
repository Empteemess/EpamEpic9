using System.Net;
using Domain.CustomExceptions;
using Microsoft.AspNetCore.Authentication;

namespace Web.Api.MiddleWares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (AuthException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = exception.Message
            });
        }
        catch (GameException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = exception.Message
            });
        }
        catch (GenreException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = exception.Message
            });
        }
        catch (PlatformException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = exception.Message
            });
        }
        catch (PublisherException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = exception.Message
            });
        }
        catch (AuthenticationFailureException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = exception.Message
            });
        }
    }
}