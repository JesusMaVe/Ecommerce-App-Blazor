using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Models;

public class Product
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
        
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
        
    [StringLength(1000)]
    public string? Description { get; set; }
        
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
        
    [Required]
    public int StockQuantity { get; set; }
        
    [StringLength(500)]
    public string? ImageUrl { get; set; }
        
    [Required]
    public Guid CategoryId { get; set; }
        
    public bool IsActive { get; set; } = true;
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    public DateTime? UpdatedAt { get; set; }
        
    // Navigation properties
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; } = null!;
        
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}