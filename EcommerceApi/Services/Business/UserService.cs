using EcommerceApi.Data.UnitOfWork;
using EcommerceApi.DTOs.Common;
using EcommerceApi.DTOs.User;
using EcommerceApi.Models;
using EcommerceApi.Services.Security;

namespace EcommerceApi.Services.Business;

public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        
        public UserService(IUnitOfWork unitOfWork, IPasswordService passwordService, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }
        
        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
                
                if (user == null || !_passwordService.VerifyPassword(loginDto.Password, user.PasswordHash))
                {
                    return ApiResponse<AuthResponseDto>.ErrorResponse("Email o contraseña incorrectos");
                }
                
                if (!user.IsActive)
                {
                    return ApiResponse<AuthResponseDto>.ErrorResponse("La cuenta está desactivada");
                }
                
                var token = _jwtService.GenerateToken(user);
                var response = new AuthResponseDto
                {
                    Token = token,
                    User = MapToUserDto(user),
                    ExpiresAt = _jwtService.GetTokenExpiration(token)
                };
                
                return ApiResponse<AuthResponseDto>.SuccessResponse(response, "Login exitoso");
            }
            catch (Exception ex)
            {
                return ApiResponse<AuthResponseDto>.ErrorResponse($"Error durante el login: {ex.Message}");
            }
        }
        
        public async Task<ApiResponse<UserDto>> RegisterAsync(CreateUserDto createUserDto)
        {
            try
            {
                var existingUser = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == createUserDto.Email);
                if (existingUser != null)
                {
                    return ApiResponse<UserDto>.ErrorResponse("Ya existe un usuario con este email");
                }
                
                var user = new User
                {
                    FirstName = createUserDto.FirstName,
                    LastName = createUserDto.LastName,
                    Email = createUserDto.Email,
                    PasswordHash = _passwordService.HashPassword(createUserDto.Password),
                    PhoneNumber = createUserDto.PhoneNumber,
                    Address = createUserDto.Address,
                    Role = UserRole.Customer,
                    IsActive = true
                };
                
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
                
                return ApiResponse<UserDto>.SuccessResponse(MapToUserDto(user), "Usuario registrado exitosamente");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.ErrorResponse($"Error durante el registro: {ex.Message}");
            }
        }
        
        public async Task<ApiResponse<UserDto>> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(id);
                if (user == null)
                {
                    return ApiResponse<UserDto>.ErrorResponse("Usuario no encontrado");
                }
                
                return ApiResponse<UserDto>.SuccessResponse(MapToUserDto(user));
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.ErrorResponse($"Error al obtener usuario: {ex.Message}");
            }
        }
        
        public async Task<ApiResponse<UserDto>> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(id);
                if (user == null)
                {
                    return ApiResponse<UserDto>.ErrorResponse("Usuario no encontrado");
                }
                
                user.FirstName = updateUserDto.FirstName;
                user.LastName = updateUserDto.LastName;
                user.PhoneNumber = updateUserDto.PhoneNumber;
                user.Address = updateUserDto.Address;
                user.UpdatedAt = DateTime.UtcNow;
                
                _unitOfWork.Users.Update(user);
                await _unitOfWork.SaveChangesAsync();
                
                return ApiResponse<UserDto>.SuccessResponse(MapToUserDto(user), "Usuario actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.ErrorResponse($"Error al actualizar usuario: {ex.Message}");
            }
        }
        
        public async Task<ApiResponse<bool>> DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(id);
                if (user == null)
                {
                    return ApiResponse<bool>.ErrorResponse("Usuario no encontrado");
                }
                
                user.IsActive = false;
                user.UpdatedAt = DateTime.UtcNow;
                
                _unitOfWork.Users.Update(user);
                await _unitOfWork.SaveChangesAsync();
                
                return ApiResponse<bool>.SuccessResponse(true, "Usuario desactivado exitosamente");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse($"Error al eliminar usuario: {ex.Message}");
            }
        }
        
        public async Task<ApiResponse<PaginatedResponse<UserDto>>> GetUsersAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var totalRecords = await _unitOfWork.Users.CountAsync(u => u.IsActive);
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                
                var users = await _unitOfWork.Users.FindAsync(u => u.IsActive);
                var paginatedUsers = users
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(MapToUserDto)
                    .ToList();
                
                var response = new PaginatedResponse<UserDto>
                {
                    Data = paginatedUsers,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    TotalRecords = totalRecords,
                    HasNextPage = pageNumber < totalPages,
                    HasPreviousPage = pageNumber > 1
                };
                
                return ApiResponse<PaginatedResponse<UserDto>>.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                return ApiResponse<PaginatedResponse<UserDto>>.ErrorResponse($"Error al obtener usuarios: {ex.Message}");
            }
        }
        
        private static UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Role = user.Role.ToString(),
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }
    }