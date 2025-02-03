using Application.Dtos.Auth;
using Application.Dtos.User;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public UsersController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        await _authService.Register(registerDto);
        return Ok("registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _authService.Login(loginDto);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
    {
        await _userService.UpdateUser(updateUserDto);
        return Ok("Updated successfully");
    }

    [HttpGet("{id}/roles")]
    public async Task<IActionResult> GetRolesByUserId(string id)
    {
        var roles = await _userService.GetRolesByUserIdAsync(id);
        return Ok(roles);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        var result = await _userService.GetAllUserAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUSerById(string id)
    {
        await _userService.DeleteUserByIdAsync(id);
        return Ok("Deleted successfully");
    }
}