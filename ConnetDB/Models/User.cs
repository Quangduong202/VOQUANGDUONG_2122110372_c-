using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public int Role { get; set; }

    public string Avatar { get; set; } = string.Empty;

    [Column("phone")]
    public int Phone { get; set; }
}