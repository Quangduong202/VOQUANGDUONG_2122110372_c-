using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Role { get; set; } = string.Empty; // admin, customer

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        //public ICollection<Order>? Orders { get; set; }
        //public ICollection<Cart>? Carts { get; set; }
        //public ICollection<Review>? Reviews { get; set; }
    }
}
