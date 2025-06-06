using EcommerceApi.DTOs.Cart;
using EcommerceApi.DTOs.Common;

namespace EcommerceApi.Services.Business;

public interface ICartService
{
    Task<ApiResponse<CartSummaryDto>> GetCartAsync(Guid userId);
    Task<ApiResponse<CartSummaryDto>> AddToCartAsync(Guid userId, AddToCartDto dto);
    Task<ApiResponse<CartSummaryDto>> UpdateCartItemAsync(Guid userId, Guid cartItemId, UpdateCartItemDto dto);
    Task<ApiResponse<bool>> RemoveCartItemAsync(Guid userId, Guid cartItemId);
    Task<ApiResponse<bool>> ClearCartAsync(Guid userId);
}