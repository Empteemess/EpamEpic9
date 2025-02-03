namespace Application.Dtos.Role;

public class AddRoleDto
{
    public string RoleName { get; set; }
    public IEnumerable<string> Permissions { get; set; }
}