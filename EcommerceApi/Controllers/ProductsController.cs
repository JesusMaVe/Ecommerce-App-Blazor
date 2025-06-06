using EcommerceApi.DTOs.Common;
using EcommerceApi.DTOs.Product;
using EcommerceApi.Services.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers;

 [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PaginatedResponse<ProductDto>>>> GetProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] Guid? categoryId = null)
        {
            if (pageNumber < 1 || pageSize < 1 || pageSize > 100)
            {
                return BadRequest(ApiResponse<PaginatedResponse<ProductDto>>.ErrorResponse("Parámetros de paginación inválidos"));
            }
            
            var result = await _productService.GetProductsAsync(pageNumber, pageSize, search, categoryId);
            
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<ProductDto>>> GetProduct(Guid id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            
            if (!result.Success)
            {
                return NotFound(result);
            }
            
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<ProductDto>.ErrorResponse("Datos inválidos", errors));
            }
            
            var result = await _productService.CreateProductAsync(createProductDto);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }
            
            return Created("", result);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateProductDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<ProductDto>.ErrorResponse("Datos inválidos", errors));
            }
            
            var result = await _productService.UpdateProductAsync(id, updateProductDto);
            
            if (!result.Success)
            {
                return NotFound(result);
            }
            
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProductAsync(id);
            
            if (!result.Success)
            {
                return NotFound(result);
            }
            
            return Ok(result);
        }
    }