using System.ComponentModel.DataAnnotations;  

namespace connetdb.Models
{
    public class ProductDetail
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Required]
        public string Size { get; set; } = string.Empty;

        [Required]
        public string Warranty { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}