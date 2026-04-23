using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Image { get; set; } = string.Empty;

        public int Status { get; set; }
    }
}