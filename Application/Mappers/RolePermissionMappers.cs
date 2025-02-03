using Application.Dtos.RolePermission;
using Domain.Entities;

namespace Application.Mappers;

public static class RolePermissionMappers
{
    public static GetRolePermissionDto ToRolePermissionDto(this IEnumerable<RolePermission> rolePermission)
    {
        return new GetRolePermissionDto
        {
            RoleName = rolePermission.Select(x => x.UserRole.Name).First(),
            Permissions = rolePermission.Select(x => x.Permission.Name),
        };
    }
}