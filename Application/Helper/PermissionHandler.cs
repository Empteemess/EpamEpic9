using System.Security.Claims;
using Application.Mappers;
using Azure;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Helper;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceProvider _serviceProvider;

    public PermissionHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        //User Role
        var role = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

        using var scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        
        //Permissions by given Role
        var rolePermissions =
            await unitOfWork.RolePermissionRepository.GetRolePermissionsByRoleNameAsync(role);

        if (!rolePermissions.Any())
        {
            context.Fail();
            return;
        }

       var res = rolePermissions.ToRolePermissionDto();
       
       //Permission
       var checkPermission = res.Permissions.Any(p => p == requirement.Permission);

       if (checkPermission)
       {
           context.Succeed(requirement);
           return;
       }
       
       context.Fail();
    }
}