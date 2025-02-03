using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IPermissionRepository
{
    Task<Permission> GetPermissionByName(string permissionName);
    Task CreatePermissionAsync(Permission permission);
    Task<IEnumerable<Permission>> GetAllPermission();
}