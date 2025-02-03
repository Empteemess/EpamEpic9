using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IRolePermissionRepository
{
    Task<IEnumerable<RolePermission>> GetRolePermissionsByRoleNameAsync(string roleName);
    Task<IEnumerable<RolePermission>> GetRolePermissionsByRoleIdAsync(string roleId);
    Task AddRolePermissionAsync(RolePermission rolePermission);
    Task<IEnumerable<string>> GetPermissionsByRoleNamesAsync(IEnumerable<string> roleNames);
}