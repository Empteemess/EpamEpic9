using Application.Dtos.Auth;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Mappers;

public static class IdentityUserMappers
{
    public static ApplicationUser ToApplicationUser(this RegisterDto registerDto)
    {
        return new ApplicationUser
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };
    }
}