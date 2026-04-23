using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Image { get; set; } = string.Empty;

        //public ICollection<Product>? Products { get; set; }
    }
}
