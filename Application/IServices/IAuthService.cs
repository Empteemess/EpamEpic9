using Application.Dtos.Auth;

namespace Application.Services;

public interface IAuthService
{
    Task<string> Login(LoginDto loginDto);
    Task Register(RegisterDto registerDto);
}