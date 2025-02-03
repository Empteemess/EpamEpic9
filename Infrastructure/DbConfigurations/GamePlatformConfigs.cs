using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations;

public class GamePlatformConfigs : IEntityTypeConfiguration<GamePlatform>
{
    public void Configure(EntityTypeBuilder<GamePlatform> builder)
    {
        builder
            .HasKey(key => key.Id);

        builder
            .HasIndex(indx => new { indx.GameId, indx.PlatformId })
            .IsUnique();

        builder
            .HasOne(g => g.Game)
            .WithMany(gp => gp.GamePlatforms)
            .HasForeignKey(fk => fk.GameId);
        
        builder
            .HasOne(g => g.Platform)
            .WithMany(gp => gp.GamePlatforms)
            .HasForeignKey(fk => fk.PlatformId);
    }
}