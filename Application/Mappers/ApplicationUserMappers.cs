using Application.Dtos.User;
using Domain.Entities;

namespace Application.Mappers;

public static class ApplicationUserMappers
{
    public static GetUserDto ToGetUserDto(this ApplicationUser applicationUsers)
    {
        return new GetUserDto
        {
            Id = applicationUsers.Id,
            Name = $"{applicationUsers.FirstName} {applicationUsers.LastName}"
        };
    }

    public static void ToUpdatedUserDto(this ApplicationUser applicationUsers,UpdateUserDto updateUserDto)
    {

        applicationUsers.Id = updateUserDto.UpdateId;
        applicationUsers.FirstName = updateUserDto.FirstName;
        applicationUsers.LastName = updateUserDto.LastName;
        applicationUsers.Email = updateUserDto.Email;
    }

}