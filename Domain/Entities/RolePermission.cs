namespace Domain.Entities;

public class RolePermission
{
    public Guid Id { get; set; }
    public required string RoleId { get; set; }
    public UserRole? UserRole { get; set; }
    
    public required Guid PermissionId { get; set; }
    public Permission? Permission { get; set; }
}