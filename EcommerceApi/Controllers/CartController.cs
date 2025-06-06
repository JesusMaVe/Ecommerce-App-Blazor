using EcommerceApi.DTOs.Cart;
using EcommerceApi.DTOs.Common;
using EcommerceApi.Services.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ApiResponse<CartSummaryDto>>> GetCart()
    {
        var userId = Guid.NewGuid(); // Simulación
        var result = await _cartService.GetCartAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponse<CartSummaryDto>>> AddToCart([FromBody] AddToCartDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<CartSummaryDto>.ErrorResponse("Datos inválidos", errors));
        }

        var userId = Guid.NewGuid(); // Simulación
        var result = await _cartService.AddToCartAsync(userId, dto);
        return Ok(result);
    }

    [HttpPut("{cartItemId}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<CartSummaryDto>>> UpdateCartItem(Guid cartItemId, [FromBody] UpdateCartItemDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<CartSummaryDto>.ErrorResponse("Datos inválidos", errors));
        }

        var userId = Guid.NewGuid(); // Simulación
        var result = await _cartService.UpdateCartItemAsync(userId, cartItemId, dto);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{cartItemId}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> RemoveCartItem(Guid cartItemId)
    {
        var userId = Guid.NewGuid(); // Simulación
        var result = await _cartService.RemoveCartItemAsync(userId, cartItemId);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("clear")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> ClearCart()
    {
        var userId = Guid.NewGuid(); // Simulación
        var result = await _cartService.ClearCartAsync(userId);
        return Ok(result);
    }
}
