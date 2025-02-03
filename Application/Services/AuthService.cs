using System.Net;
using System.Text;
using Application.Dtos.Auth;
using Application.Mappers;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IJwtService jwtService, IUnitOfWork unitOfWork)
    {
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
    }

    public async Task Register(RegisterDto registerDto)
    {
        await CheckRoles(registerDto);

        var user = await CreateUser(registerDto);

        await AssignRolesToUser(user, registerDto.RoleNames);
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        var user = await _unitOfWork.UserManager.FindByEmailAsync(loginDto.Email);
        if (user is null) throw new AuthException($"{loginDto.Email} not found", (int)HttpStatusCode.NotFound);

        var checkPassword = await _unitOfWork.UserManager.CheckPasswordAsync(user, loginDto.Password);
        if (!checkPassword) throw new AuthException($"Password is wrong", (int)HttpStatusCode.Forbidden);

        var roles = await _unitOfWork.UserManager.GetRolesAsync(user);
        if (roles is null) throw new AuthException("Role not found", (int)HttpStatusCode.NotFound);

        var permissions = await _unitOfWork.RolePermissionRepository.GetPermissionsByRoleNamesAsync(roles);
        if (permissions.Count() <= 0) permissions = ["Default"];

        var token = await _jwtService.CreateJwtToken(user, roles, permissions);

        return token;
    }

    #region Register

    private async Task AssignRolesToUser(ApplicationUser user, IEnumerable<string> roles)
    {
        var roleAdd = await _unitOfWork.UserManager.AddToRolesAsync(user, roles);

        if (!roleAdd.Succeeded)
        {
            throw new AuthException("Roles Cant't be created", Convert.ToInt16(roleAdd.Errors.Select(x => x.Code)));
        }
    }

    private async Task<ApplicationUser> CreateUser(RegisterDto registerDto)
    {
        var user = registerDto.ToApplicationUser();

        var userCreated = await _unitOfWork.UserManager.CreateAsync(user, registerDto.Password);
        if (!userCreated.Succeeded)
            throw new AuthException($"{userCreated.Errors.First().Description}",
                (int)HttpStatusCode.Created);

        return user;
    }

    private async Task CheckRoles(RegisterDto registerDto)
    {
        foreach (var roleName in registerDto.RoleNames)
        {
            var checkRole = await _unitOfWork.RoleManager.FindByNameAsync(roleName);
            if (checkRole is null) throw new AuthException($"Role {roleName} does not exist");
        }
    }

    #endregion
}