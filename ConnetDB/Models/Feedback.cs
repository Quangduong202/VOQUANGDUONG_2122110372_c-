using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int Star { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}