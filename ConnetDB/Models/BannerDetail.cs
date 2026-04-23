using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class BannerDetail
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int BannerId { get; set; }
        public Banner? Banner { get; set; }
    }
}