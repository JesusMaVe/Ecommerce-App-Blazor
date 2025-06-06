using EcommerceApi.DTOs.Order;
using EcommerceApi.DTOs.Common;
using EcommerceApi.Services.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<ApiResponse<List<OrderDto>>>> GetOrders()
    {
        var result = await _orderService.GetOrdersAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<OrderDto>>> GetOrder(Guid id)
    {
        var result = await _orderService.GetOrderByIdAsync(id);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponse<OrderDto>>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<OrderDto>.ErrorResponse("Datos inválidos", errors));
        }

        // Simulación: en un entorno real se obtiene el userId desde el token
        var userId = Guid.NewGuid(); // Sustituir con: User.FindFirst(ClaimTypes.NameIdentifier)?.Value

        var result = await _orderService.CreateOrderAsync(userId, createOrderDto);

        if (!result.Success)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetOrder), new { id = result.Data!.Id }, result);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusDto updateOrderDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<OrderDto>.ErrorResponse("Datos inválidos", errors));
        }

        var result = await _orderService.UpdateOrderStatusAsync(id, updateOrderDto);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteOrder(Guid id)
    {
        var result = await _orderService.DeleteOrderAsync(id);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }
}
