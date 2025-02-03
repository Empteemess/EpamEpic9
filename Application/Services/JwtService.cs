using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> CreateJwtToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions)
    {
        //GenerateClaims
        var claims = GenerateClaims(user, roles, permissions);

        //GenerateSigningKey
        var signingKey = GetSigningKey();

        //CreateJwToken
        var result = GenerateJwtToken(claims, signingKey);

        return result;
    }

    private string GenerateJwtToken(IEnumerable<Claim> claims, SigningCredentials signingCredential)
    {
        var jwtToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredential,
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["EXPIRE_DATE"]))
        );

        return new JwtSecurityTokenHandler()
            .WriteToken(jwtToken);
    }

    private IEnumerable<Claim> GenerateClaims(ApplicationUser user, IEnumerable<string> roleNames,
        IEnumerable<string> permissions)
    {
        var claim = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email),
        };

        claim.AddRange(roleNames.Select(role => new Claim(ClaimTypes.Role, role)));
        claim.AddRange(permissions.Select(permission => new Claim("Permission", permission)));

        return claim;
    }

    private SigningCredentials GetSigningKey()
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT_KEY"]));
        return new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
    }
}