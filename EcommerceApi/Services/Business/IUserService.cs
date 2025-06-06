using EcommerceApi.DTOs.Common;
using EcommerceApi.DTOs.User;

namespace EcommerceApi.Services.Business;

public interface IUserService
{
    Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto);
    Task<ApiResponse<UserDto>> RegisterAsync(CreateUserDto createUserDto);
    Task<ApiResponse<UserDto>> GetUserByIdAsync(Guid id);
    Task<ApiResponse<UserDto>> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto);
    Task<ApiResponse<bool>> DeleteUserAsync(Guid id);
    Task<ApiResponse<PaginatedResponse<UserDto>>> GetUsersAsync(int pageNumber = 1, int pageSize = 10);
}