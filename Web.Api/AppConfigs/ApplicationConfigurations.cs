using Application.IServices;
using Application.Services;
using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Data;
using Infrastructure.Helper;
using Infrastructure.PipelineSteps;
using Infrastructure.Repositories;
using Infrastructure.Seeder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Api.MiddleWares;

namespace Web.Api.AppConfigs;

public static class ApplicationConfigurations
{
    public static IServiceCollection AddApplicationConfigs(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<Seeder>();

        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IPlatformRepository, PlatformRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IPaymentsRepository, PaymentsRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IBanDurationRepository, BanDurationRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        
        services.AddSingleton<IAuthorizationHandler,PermissionHandler>();

        services.AddScoped<UserManager<ApplicationUser>>();
        services.AddScoped<RoleManager<UserRole>>();

        services.AddScoped<IGameServices, GameService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IPlatformService, PlatformService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddScoped<ExceptionHandlerMiddleware>();

        return services;
    }
}