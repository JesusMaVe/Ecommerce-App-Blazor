using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs.Cart;

public class CartItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public string? ProductImageUrl { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}
    
public class AddToCartDto
{
    [Required(ErrorMessage = "El ID del producto es requerido")]
    public Guid ProductId { get; set; }
        
    [Required(ErrorMessage = "La cantidad es requerida")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Quantity { get; set; }
}
    
public class UpdateCartItemDto
{
    [Required(ErrorMessage = "La cantidad es requerida")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Quantity { get; set; }
}
    
public class CartSummaryDto
{
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    public int TotalItems { get; set; }
    public decimal TotalAmount { get; set; }
}