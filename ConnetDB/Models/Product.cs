using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace connetdb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Image { get; set; } = string.Empty;

        public int Price { get; set; }
        public int OldPrice { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public string Specification { get; set; } = string.Empty;
        public string BuyTurn { get; set; } = string.Empty;

        public int Quantity { get; set; }

        // FK
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Navigation
        public ICollection<ProductImage>? Images { get; set; }
    }
}