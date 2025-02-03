using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class UserRole : IdentityRole
{
    public ICollection<RolePermission>? RolePermissions { get; set; }
}