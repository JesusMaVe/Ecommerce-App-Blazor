using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models;

public class Category
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
        
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
        
    [StringLength(500)]
    public string? Description { get; set; }
        
    public bool IsActive { get; set; } = true;
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    public DateTime? UpdatedAt { get; set; }
        
    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}