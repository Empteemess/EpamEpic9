using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context,
        IGameRepository gameRepository,
        ICommentRepository commentRepository,
        IGenreRepository genreRepository,
        IPlatformRepository platformRepository,
        IPublisherRepository publisherRepository,
        ICartRepository cartRepository,
        IPaymentsRepository paymentRepository,
        IBanDurationRepository banDurationRepository,
        IRolePermissionRepository rolePermissionRepository,
        UserManager<ApplicationUser> userManager,
        RoleManager<UserRole> roleManager,
        IPermissionRepository permissionRepository)
    {
        _context = context;
        banDurationRepository = banDurationRepository;
        UserManager = userManager;
        RoleManager = roleManager;
        RolePermissionRepository = rolePermissionRepository;
        PaymentsRepository = paymentRepository;
        CartRepository = cartRepository;
        PublisherRepository = publisherRepository;
        PlatformRepository = platformRepository;
        GameRepository = gameRepository;
        GenreRepository = genreRepository;
        CommentRepository = commentRepository;
        PermissionRepository = permissionRepository;
    }

    public IPermissionRepository PermissionRepository { get; }
    public UserManager<ApplicationUser> UserManager { get; }
    public RoleManager<UserRole> RoleManager { get; }
    public IRolePermissionRepository RolePermissionRepository { get; }
    public IBanDurationRepository BanDurationRepository { get; }
    public ICommentRepository CommentRepository { get; }
    public ICartRepository CartRepository { get; }
    public IPaymentsRepository PaymentsRepository { get; }
    public IPublisherRepository PublisherRepository { get; }
    public IGameRepository GameRepository { get; }
    public IGenreRepository GenreRepository { get; }
    public IPlatformRepository PlatformRepository { get; }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}