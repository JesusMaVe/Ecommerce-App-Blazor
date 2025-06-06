using EcommerceApi.DTOs.Category;
using EcommerceApi.DTOs.Common;

namespace EcommerceApi.Services.Business;

public interface ICategoryService
{
    Task<ApiResponse<List<CategoryDto>>> GetCategoriesAsync();
    Task<ApiResponse<CategoryDto>> GetCategoryByIdAsync(Guid id);
    Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryDto updateCategoryDto);
    Task<ApiResponse<bool>> DeleteCategoryAsync(Guid id);
}