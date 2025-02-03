using Application.Dtos.Role;
using Application.Dtos.RolePermission;

namespace Application.Services;

public interface IRoleService
{
    Task<GetRolePermissionDto> GetRolePermissionByIdAsync(string roleId);
    Task AddRoleAsync(AddRoleDto addRoleDto);
    Task<IEnumerable<string>> GetAllPermissionsAsync();
    Task CreatePermissionAsync(string permissionName);
    Task<IEnumerable<GetRoleDto>> GetAllRoleAsync();
    Task<GetRoleDto> GetRoleByIdAsync(string roleId);
    Task DeleteRoleById(string roleId);
}