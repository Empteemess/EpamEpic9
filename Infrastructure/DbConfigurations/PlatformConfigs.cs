using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations;

public class PlatformConfigs : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder
            .HasKey(key => key.Id);
        
        builder
            .HasIndex(indx => new {indx.Id,indx.Type})
            .IsUnique();
    }
}