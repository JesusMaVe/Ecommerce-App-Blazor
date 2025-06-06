using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
        
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
        
    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;
        
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
        
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
        
    [StringLength(20)]
    public string? PhoneNumber { get; set; }
        
    [StringLength(500)]
    public string? Address { get; set; }
        
    public UserRole Role { get; set; } = UserRole.Customer;
        
    public bool IsActive { get; set; } = true;
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    public DateTime? UpdatedAt { get; set; }
        
    // Navigation properties
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
    
public enum UserRole
{
    Customer = 0,
    Admin = 1,
    SuperAdmin = 2
}