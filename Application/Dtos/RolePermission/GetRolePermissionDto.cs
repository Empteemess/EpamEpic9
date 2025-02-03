namespace Application.Dtos.RolePermission;

public class GetRolePermissionDto
{
    public string RoleName { get; set; }
    public IEnumerable<string> Permissions { get; set; }
}