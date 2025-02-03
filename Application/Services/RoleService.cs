using System.Net;
using Application.Dtos.Role;
using Application.Dtos.RolePermission;
using Application.Mappers;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<string>> GetAllPermissionsAsync()
    {
        var permissions = await _unitOfWork.PermissionRepository.GetAllPermission();
        return permissions.Select(p => p.Name).ToList();
    }

    public async Task<GetRolePermissionDto> GetRolePermissionByIdAsync(string roleId)
    {
        var rolePermisions = await _unitOfWork.RolePermissionRepository.GetRolePermissionsByRoleIdAsync(roleId);
        if(rolePermisions.Count() <= 0) throw new AuthException("Role permission not found", (int)HttpStatusCode.NotFound);
        
        return rolePermisions.ToRolePermissionDto();
    }
    
    public async Task CreatePermissionAsync(string permissionName)
    {
        var permissionCheck = await _unitOfWork.PermissionRepository.GetPermissionByName(permissionName);
        if (permissionCheck != null)
            throw new AuthException($"Permission {permissionName} already exists", (int)HttpStatusCode.Created);

        await _unitOfWork.PermissionRepository.CreatePermissionAsync(new Permission { Name = permissionName });
        await _unitOfWork.SaveAsync();
    }

    public async Task AddRoleAsync(AddRoleDto addRoleDto)
    {
        //Check Role
        var roleCheck = await _unitOfWork.RoleManager.FindByNameAsync(addRoleDto.RoleName);
        if (roleCheck is not null)
            throw new AuthException($"Role {addRoleDto.RoleName} already Exists", StatusCodes.Status201Created);

        //Create Role
        var role = new UserRole()
            { Name = addRoleDto.RoleName, ConcurrencyStamp = DateTime.Now.Millisecond.ToString() };

        //Check permission
        foreach (var permission in addRoleDto.Permissions)
        {
            var permissionCheck = await _unitOfWork.PermissionRepository.GetPermissionByName(permission);
            if (permission is null) throw new AuthException("Permission not found", (int)HttpStatusCode.NotFound);
        }

        //Create Role
        await _unitOfWork.RoleManager.CreateAsync(role);

        //Add Role Permision
        foreach (var permission in addRoleDto.Permissions)
        {
            var permissionCheck = await _unitOfWork.PermissionRepository.GetPermissionByName(permission);
            var rolePermission = new RolePermission()
            {
                Id = Guid.NewGuid(),
                RoleId = role.Id,
                PermissionId = permissionCheck.Id
            };

            await _unitOfWork.RolePermissionRepository.AddRolePermissionAsync(rolePermission);
        }

        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<GetRoleDto>> GetAllRoleAsync()
    {
        var roles = await _unitOfWork.RoleManager.Roles.ToListAsync();
        if (roles is null) return new List<GetRoleDto>();

        return roles.Select(x => x.ToGetRoleDto());
    }

    public async Task<GetRoleDto> GetRoleByIdAsync(string roleId)
    {
        var roles = await _unitOfWork.RoleManager.FindByIdAsync(roleId);
        if (roles is null) throw new AuthException($"Role{roleId} not found", StatusCodes.Status404NotFound);

        return roles.ToGetRoleDto();
    }

    public async Task DeleteRoleById(string roleId)
    {
        var roles = await _unitOfWork.RoleManager.FindByIdAsync(roleId);
        if (roles is null) throw new AuthException($"Role{roleId} not found", StatusCodes.Status404NotFound);

        await _unitOfWork.RoleManager.DeleteAsync(roles);
    }
}