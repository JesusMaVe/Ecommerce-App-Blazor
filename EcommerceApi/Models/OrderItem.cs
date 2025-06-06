using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Models;

public class OrderItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
        
    [Required]
    public Guid OrderId { get; set; }
        
    [Required]
    public Guid ProductId { get; set; }
        
    [Required]
    public int Quantity { get; set; }
        
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }
        
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice => Quantity * UnitPrice;
        
    // Navigation properties
    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; } = null!;
        
    [ForeignKey("ProductId")]  
    public virtual Product Product { get; set; } = null!;
}