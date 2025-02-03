using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Permission
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<RolePermission>? RolePermissions { get; set; }
}