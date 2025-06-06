using EcommerceApi.DTOs.Common;
using EcommerceApi.DTOs.User;
using EcommerceApi.Services.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
        
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
        
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PaginatedResponse<UserDto>>>> GetUsers(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1 || pageSize > 100)
        {
            return BadRequest(ApiResponse<PaginatedResponse<UserDto>>.ErrorResponse("Parámetros de paginación inválidos"));
        }
            
        var result = await _userService.GetUsersAsync(pageNumber, pageSize);
            
        return Ok(result);
    }
        
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetUser(Guid id)
    {
        var result = await _userService.GetUserByIdAsync(id);
            
        if (!result.Success)
        {
            return NotFound(result);
        }
            
        return Ok(result);
    }
        
    [HttpDelete("{id}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteUser(Guid id)
    {
        var result = await _userService.DeleteUserAsync(id);
            
        if (!result.Success)
        {
            return NotFound(result);
        }
            
        return Ok(result);
    }
}