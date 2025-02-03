using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations;

public class RolePermissionConfigs : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder
            .HasIndex(x => new { x.PermissionId, x.RoleId })
            .IsUnique();
        
        builder
            .HasOne(x => x.Permission)
            .WithMany(x => x.RolePermissions)
            .HasForeignKey(x => x.PermissionId);
        
        builder
            .HasOne(x => x.UserRole)
            .WithMany(x => x.RolePermissions)
            .HasForeignKey(x => x.RoleId);
    }
}