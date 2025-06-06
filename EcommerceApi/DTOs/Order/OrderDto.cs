using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs.Order;

public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ShippingAddress { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
    
    public class CreateOrderDto
    {
        [StringLength(500, ErrorMessage = "La dirección de envío no puede exceder 500 caracteres")]
        public string? ShippingAddress { get; set; }
        
        [StringLength(1000, ErrorMessage = "Las notas no pueden exceder 1000 caracteres")]
        public string? Notes { get; set; }
        
        [Required(ErrorMessage = "Los items del pedido son requeridos")]
        [MinLength(1, ErrorMessage = "Debe incluir al menos un item")]
        public List<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
    }
    
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
    
    public class CreateOrderItemDto
    {
        [Required(ErrorMessage = "El ID del producto es requerido")]
        public Guid ProductId { get; set; }
        
        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Quantity { get; set; }
    }
    
    public class UpdateOrderStatusDto
    {
        [Required(ErrorMessage = "El estado es requerido")]
        public int Status { get; set; }
        
        [StringLength(1000, ErrorMessage = "Las notas no pueden exceder 1000 caracteres")]
        public string? Notes { get; set; }
    }