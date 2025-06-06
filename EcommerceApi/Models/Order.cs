using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
        
    [Required]
    public Guid UserId { get; set; }
        
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
        
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
    [StringLength(500)]
    public string? ShippingAddress { get; set; }
        
    [StringLength(1000)]
    public string? Notes { get; set; }
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    public DateTime? UpdatedAt { get; set; }
        
    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
        
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
    
public enum OrderStatus
{
    Pending = 0,
    Processing = 1,
    Shipped = 2,
    Delivered = 3,
    Cancelled = 4
}