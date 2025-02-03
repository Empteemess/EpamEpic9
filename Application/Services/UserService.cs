using System.Net;
using Application.Dtos.Role;
using Application.Dtos.User;
using Application.Mappers;
using Domain.CustomExceptions;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task UpdateUser(UpdateUserDto updateUserDto)
    {
        //Find User
        var user = await _unitOfWork.UserManager.FindByIdAsync(updateUserDto.UpdateId);
        if (user is null) throw new AuthException($"User {updateUserDto.UpdateId} not found", StatusCodes.Status404NotFound);

        //Check Pasword
        var ss = await _unitOfWork.UserManager.ChangePasswordAsync(user,updateUserDto.CurrentPassword,updateUserDto.NewPassword);
        if (!ss.Succeeded) throw new AuthException($"Password is wrong", (int)HttpStatusCode.Forbidden);
        
        //Remove Roles
        var roles = await _unitOfWork.UserManager.GetRolesAsync(user);
        if (roles is not null)
        {
            await _unitOfWork.UserManager.RemoveFromRolesAsync(user, roles);
        }

        //Add new Roles
        await _unitOfWork.UserManager.AddToRolesAsync(user, updateUserDto.RoleNames);

        //Update User
        user.ToUpdatedUserDto(updateUserDto);
        await _unitOfWork.UserManager.UpdateAsync(user);
        await _unitOfWork.SaveAsync();
    }
    public async Task<IEnumerable<GetRoleDto>> GetRolesByUserIdAsync(string userId)
    {
        var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
        var roles = await _unitOfWork.UserManager.GetRolesAsync(user);

        var wholeRolesl = roles
            .Select(x => _unitOfWork.RoleManager.FindByNameAsync(x).Result)
            .Select(x => x.ToGetRoleDto());
        
        return wholeRolesl;
    }
    public async Task<IEnumerable<GetUserDto>> GetAllUserAsync()
    {
        var users = _unitOfWork.UserManager.Users;
        if (users is null) return new List<GetUserDto>();

        return users.Select(x => x.ToGetUserDto());
    }

    public async Task<GetUserDto> GetUserByIdAsync(string userId)
    {
        var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
        if (user is null) throw new AuthException($"User {userId} not found", StatusCodes.Status404NotFound);

        return user.ToGetUserDto();
    }

    public async Task DeleteUserByIdAsync(string userId)
    {
        var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
        if (user is null) throw new AuthException($"User {userId} not found", StatusCodes.Status404NotFound);

        await _unitOfWork.UserManager.DeleteAsync(user);
        await _unitOfWork.SaveAsync();
    }
}