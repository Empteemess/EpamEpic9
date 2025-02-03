using DotNetEnv;
using Infrastructure.Seeder;
using Microsoft.AspNetCore.Diagnostics;
using Web.Api.AppConfigs;
using Web.Api.MiddleWares;
using ExceptionHandlerMiddleware = Web.Api.MiddleWares.ExceptionHandlerMiddleware;

namespace Web.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        Env.Load();
        
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddEnvironmentVariables();
        builder.Services.AddControllers();

        builder.Services.AddApplicationConfigs();
        builder.Services.AddServiceCollection(builder.Configuration);
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        using var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
        await seeder.Seed();
        
        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseMiddleware<TotalGamesHeaderMiddleware>();
        
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}