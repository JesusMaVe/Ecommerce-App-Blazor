using System.Security.Claims;
using EcommerceApi.Models;

namespace EcommerceApi.Services.Security;

public interface IJwtService
{
    string GenerateToken(User user);
    ClaimsPrincipal? ValidateToken(string token);
    DateTime GetTokenExpiration(string token);
}