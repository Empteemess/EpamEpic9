using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations;

public class PublisherConfigs : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder
            .HasIndex(ind => ind.CompanyName)
            .IsUnique();

        builder
            .HasMany(g => g.Games)
            .WithOne(p => p.Publisher)
            .HasForeignKey(pi => pi.PublisherId)
            .IsRequired();
    }
}