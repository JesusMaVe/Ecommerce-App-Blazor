using System.Security.Claims;
using EcommerceApi.DTOs.Common;
using EcommerceApi.DTOs.User;
using EcommerceApi.Services.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers;

[ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<AuthResponseDto>.ErrorResponse("Datos inválidos", errors));
            }
            
            var result = await _userService.LoginAsync(loginDto);
            
            if (!result.Success)
            {
                return Unauthorized(result);
            }
            
            return Ok(result);
        }
        
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<UserDto>>> Register([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<UserDto>.ErrorResponse("Datos inválidos", errors));
            }
            
            var result = await _userService.RegisterAsync(createUserDto);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }
            
            return Created("", result);
        }
        
        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
            {
                return Unauthorized(ApiResponse<UserDto>.ErrorResponse("Token inválido"));
            }
            
            var result = await _userService.GetUserByIdAsync(userGuid);
            
            if (!result.Success)
            {
                return NotFound(result);
            }
            
            return Ok(result);
        }
        
        [HttpPut("profile")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserDto>>> UpdateProfile([FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<UserDto>.ErrorResponse("Datos inválidos", errors));
            }
            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
            {
                return Unauthorized(ApiResponse<UserDto>.ErrorResponse("Token inválido"));
            }
            
            var result = await _userService.UpdateUserAsync(userGuid, updateUserDto);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }
            
            return Ok(result);
        }
    }