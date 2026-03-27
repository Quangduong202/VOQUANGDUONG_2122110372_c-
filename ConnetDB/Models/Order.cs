using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "pending"; // pending, paid, shipped, completed, cancelled

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
