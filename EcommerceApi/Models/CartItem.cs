using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Models;

public class CartItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
        
    [Required]
    public Guid UserId { get; set; }
        
    [Required]
    public Guid ProductId { get; set; }
        
    [Required]
    public int Quantity { get; set; }
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    public DateTime? UpdatedAt { get; set; }
        
    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
        
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; } = null!;
}