using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly DbSet<RolePermission> _rolePermissions;

    public RolePermissionRepository(AppDbContext context)
    {
        _rolePermissions = context.Set<RolePermission>();
    }

    public async Task<IEnumerable<RolePermission>> GetRolePermissionsByRoleNameAsync(string roleName)
    {
        return await _rolePermissions
            .AsNoTracking()
            .Include(x => x.UserRole)
            .Include(x => x.Permission)
            .Where(p => p.UserRole.Name == roleName)
            .ToListAsync();
    }
    public async Task<IEnumerable<RolePermission>> GetRolePermissionsByRoleIdAsync(string roleId)
    {
        return await _rolePermissions
            .AsNoTracking()
            .Include(x => x.UserRole)
            .Include(x => x.Permission)
            .Where(p => p.RoleId == roleId)
            .ToListAsync();
    }
    
    public async Task AddRolePermissionAsync(RolePermission rolePermission)
    {
        await _rolePermissions.AddAsync(rolePermission);
    }
    
    public async Task<IEnumerable<string>> GetPermissionsByRoleNamesAsync(IEnumerable<string> roleNames)
    {
        var rolePermission = _rolePermissions
            .AsNoTracking()
            .Include(x => x.Permission)
            .Include(x => x.UserRole)
            .Where(x => roleNames.Contains(x.UserRole.Name))
            .Distinct()
            .Select(x => x.Permission.Name);

        return await rolePermission.ToListAsync();
    }
}