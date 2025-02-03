using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations;

public class GenreConfigs : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(key => key.Id);
        
        builder
            .HasIndex(indx => new {indx.Id,indx.Name})
            .IsUnique();
    }
}