using EcommerceApi.DTOs.Common;
using EcommerceApi.DTOs.Order;

namespace EcommerceApi.Services.Business;

public interface IOrderService
{
    Task<ApiResponse<List<OrderDto>>> GetOrdersAsync();
    Task<ApiResponse<OrderDto>> GetOrderByIdAsync(Guid id);
    Task<ApiResponse<OrderDto>> CreateOrderAsync(Guid userId, CreateOrderDto dto);
    Task<ApiResponse<OrderDto>> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusDto dto);
    Task<ApiResponse<bool>> DeleteOrderAsync(Guid id);
}