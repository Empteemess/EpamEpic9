using System.Security.Claims;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public interface IJwtService
{
    Task<string> CreateJwtToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions);
}