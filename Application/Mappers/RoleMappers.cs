using Application.Dtos.Role;
using Domain.Entities;

namespace Application.Mappers;

public static class RoleMappers
{
    public static GetRoleDto ToGetRoleDto(this UserRole userRole)
    {
        return new GetRoleDto
        {
            Id = userRole.Id,
            Name = userRole.Name
        };
    }
}