using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository {
    private readonly DbSet<Permission> _permissions;

    public PermissionRepository(AppDbContext context)
    {
        _permissions = context.Set<Permission>();
    }

    public async Task<Permission> GetPermissionByName(string permissionName)
    {
        return await _permissions.FirstOrDefaultAsync(x => x.Name == permissionName);
    }
    public async Task CreatePermissionAsync(Permission permission)
    {
        await _permissions.AddAsync(permission);
    }

    public async Task<IEnumerable<Permission>> GetAllPermission()
    {
        return _permissions;
    }
}