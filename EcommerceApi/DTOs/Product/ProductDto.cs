using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs.Product;

public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    
    public class CreateProductDto
    {
        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "La cantidad en stock es requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 0")]
        public int StockQuantity { get; set; }
        
        [StringLength(500, ErrorMessage = "La URL de imagen no puede exceder 500 caracteres")]
        public string? ImageUrl { get; set; }
        
        [Required(ErrorMessage = "La categoría es requerida")]
        public Guid CategoryId { get; set; }
    }
    
    public class UpdateProductDto
    {
        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "La cantidad en stock es requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 0")]
        public int StockQuantity { get; set; }
        
        [StringLength(500, ErrorMessage = "La URL de imagen no puede exceder 500 caracteres")]
        public string? ImageUrl { get; set; }
        
        [Required(ErrorMessage = "La categoría es requerida")]
        public Guid CategoryId { get; set; }
        
        public bool IsActive { get; set; } = true;
    }