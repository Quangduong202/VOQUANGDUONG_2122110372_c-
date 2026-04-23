using System.ComponentModel.DataAnnotations;

namespace connetdb.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Image { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}