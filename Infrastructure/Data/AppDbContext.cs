using Domain.Entities;
using Infrastructure.DbConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : IdentityDbContext<IdentityUser,UserRole,string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderGame> OrderGames { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<BanDurations> BanDurations { get; set; }
    public DbSet<DateFilter> DateFilters { get; set; }
    public DbSet<PaginationOption> PaginationOptions { get; set; }
    public DbSet<SortingOption> SortingOptions { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<GameGenre> GameGenres { get; set; }
    public DbSet<GamePlatform> GamePlatforms { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new GameConfigs());
        modelBuilder.ApplyConfiguration(new GameGenreConfigs());
        modelBuilder.ApplyConfiguration(new GamePlatformConfigs());
        modelBuilder.ApplyConfiguration(new GenreConfigs());
        modelBuilder.ApplyConfiguration(new PlatformConfigs());
        modelBuilder.ApplyConfiguration(new PublisherConfigs());
        modelBuilder.ApplyConfiguration(new OrderGamesConfigs());
        modelBuilder.ApplyConfiguration(new RolePermissionConfigs());
    }
}