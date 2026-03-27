using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        //public ICollection<Product>? Products { get; set; }
    }
}
