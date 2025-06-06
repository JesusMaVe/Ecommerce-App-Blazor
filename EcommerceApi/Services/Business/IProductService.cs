using EcommerceApi.DTOs.Common;
using EcommerceApi.DTOs.Product;

namespace EcommerceApi.Services.Business;

public interface IProductService
{
    Task<ApiResponse<PaginatedResponse<ProductDto>>> GetProductsAsync(int pageNumber, int pageSize, string? search, Guid? categoryId);
    Task<ApiResponse<ProductDto>> GetProductByIdAsync(Guid id);
    Task<ApiResponse<ProductDto>> CreateProductAsync(CreateProductDto dto);
    Task<ApiResponse<ProductDto>> UpdateProductAsync(Guid id, UpdateProductDto dto);
    Task<ApiResponse<bool>> DeleteProductAsync(Guid id);
}