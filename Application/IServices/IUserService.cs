using Application.Dtos.Role;
using Application.Dtos.User;

namespace Application.Services;

public interface IUserService
{
    Task<IEnumerable<GetRoleDto>> GetRolesByUserIdAsync(string userId);
    Task UpdateUser(UpdateUserDto updateUserDto);
    Task<IEnumerable<GetUserDto>> GetAllUserAsync();
    Task<GetUserDto> GetUserByIdAsync(string userId);
    Task DeleteUserByIdAsync(string userId);
}