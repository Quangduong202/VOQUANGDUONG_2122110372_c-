using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty; // COD, bank, momo

        [StringLength(50)]
        public string Status { get; set; } = "pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
