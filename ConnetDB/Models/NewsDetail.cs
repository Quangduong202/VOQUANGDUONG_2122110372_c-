using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class NewsDetail
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int NewsId { get; set; }
        public News? News { get; set; }
    }
}