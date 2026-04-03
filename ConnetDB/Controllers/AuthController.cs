using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    // POST /Auth/Login
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        // Tìm user theo username
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

        // So sánh password trực tiếp (plain text)
        if (user == null || user.Password != request.Password)
        {
            return Unauthorized(new { message = "Username or password is incorrect" });
        }

        // Nếu login thành công, trả về thông tin user (chưa dùng JWT)
        return Ok(new
        {
            message = "Login successful",
            user = new
            {
                user.Id,
                user.Username,
                user.Email
            }
        });
    }
}

// DTO cho login
public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}