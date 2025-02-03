using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations;

public class GameGenreConfigs : IEntityTypeConfiguration<GameGenre>
{
    public void Configure(EntityTypeBuilder<GameGenre> builder)
    {
        builder
            .HasKey(key => key.Id);
        
        builder
            .HasIndex(indx => new {indx.GameId, indx.GenreId})
            .IsUnique();
        
        builder
            .HasOne(g => g.Game)
            .WithMany(gg => gg.GameGenres)
            .HasForeignKey(fk => fk.GameId);
        
        builder
            .HasOne(g => g.Genre)
            .WithMany(gg => gg.GameGenres)
            .HasForeignKey(fk => fk.GenreId);
    }
}