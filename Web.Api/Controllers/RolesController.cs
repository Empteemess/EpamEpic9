using Application.Dtos.Role;
using Application.Services;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost]
    public async Task<IActionResult> AddRole(AddRoleDto addRoleDto)
    {
        await _roleService.AddRoleAsync(addRoleDto);
        return Ok($"{addRoleDto.RoleName} added");
    }

    [HttpPost("permission/{permissionName}")]
    public async Task<IActionResult> CreatePermission(string permissionName)
    {
        await _roleService.CreatePermissionAsync(permissionName);
        return Ok("permission added");
    }

    [HttpGet("{id}/permissions")]
    public async Task<IActionResult> GetRolePermisionsById(string id)
    {
        var permissions = await _roleService.GetRolePermissionByIdAsync(id);
        return Ok(permissions);
    }

    [HttpGet("permissions")]
    public async Task<IActionResult> GetAllPermissions()
    {
        var permissions = await _roleService.GetAllPermissionsAsync();
        return Ok(permissions);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _roleService.GetAllRoleAsync();
        return Ok(roles);
    }

    [Authorize(Policy = "CanAddGame")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(string id)
    {
        var roles = await _roleService.GetRoleByIdAsync(id);

        var options = new CookieOptions()
        {
            Expires = DateTime.Now.AddHours(1),
            Secure = true,
            HttpOnly = true
        };
    
        var responseDto = new
        {
            AccessTokens = "AccessTokens",
            RefreshTokens = "RefreshTokens",
        };
            
        var response = JsonConvert.SerializeObject(responseDto);

        HttpContext.Response.Cookies.Append("Token",response,options);
                
        return Ok(roles);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        await _roleService.DeleteRoleById(id);
        return Ok("Role deleted");
    }
}