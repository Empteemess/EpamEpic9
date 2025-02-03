using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Domain.IRepositories;

public interface IUnitOfWork
{
    IPermissionRepository PermissionRepository { get; }
    UserManager<ApplicationUser> UserManager { get; }
    RoleManager<UserRole> RoleManager { get; }
    IRolePermissionRepository RolePermissionRepository { get; }
    IBanDurationRepository BanDurationRepository { get; }
    ICommentRepository CommentRepository { get; }
    ICartRepository CartRepository { get; }
    IPaymentsRepository PaymentsRepository { get; }
    IPublisherRepository PublisherRepository { get; }
    IGameRepository GameRepository { get; }
    IGenreRepository GenreRepository { get; }
    IPlatformRepository PlatformRepository { get; }
    Task SaveAsync();
}