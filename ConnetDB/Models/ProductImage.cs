using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Required, StringLength(255)]
        public string ImageUrl { get; set; } = string.Empty;
    }
}