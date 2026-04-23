using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        [Required, StringLength(50)]
        public string SessionId { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public int Total { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}