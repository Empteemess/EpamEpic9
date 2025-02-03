using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations;

public class OrderGamesConfigs : IEntityTypeConfiguration<OrderGame>
{
    public void Configure(EntityTypeBuilder<OrderGame> builder)
    {
        builder
            .HasIndex(x => new { x.OrderId, x.ProductId })
            .IsUnique();

        builder
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderGames)
            .HasForeignKey(x => x.OrderId)
            .IsRequired();

        builder
            .HasOne(x => x.Game)
            .WithMany(x => x.OrderGames)
            .HasForeignKey(x => x.ProductId)
            .IsRequired();
    }
}