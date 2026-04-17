using connetdb.Data;
using connetdb.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

namespace ConnetDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // 🔥 tất cả API phải login
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Chỉ Admin mới xem được danh sách user
        //[Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Password,
                    u.Email,
                    u.Role,
                    u.CreatedAt
                })
                .ToListAsync();

            return Ok(users);
        }

        // ✅ Admin hoặc chính user đó mới xem được
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var currentUser = User.Identity.Name;
            var currentRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            // 🔥 Check quyền
            //if (currentRole != "ADMIN" && user.Username != currentUser)
            //    return Forbid();

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Password,
                user.Email,
                user.Role,
                user.CreatedAt
            });
        }

        // ✅ Ai cũng đăng ký được
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            // 🔥 Hash password (QUAN TRỌNG)
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // 🔥 mặc định role = USER
            //user.Role = "USER";

            user.CreatedAt = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.Role
            });
        }
        // ✅ Chỉ ADMIN được xoá
        //[Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Xoá user thành công"
            });
        }
        // ✅ Admin hoặc chính user đó mới được sửa
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User updatedUser)
        {
            var currentUser = User.Identity?.Name;
            //var currentRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            //// 🔥 Check quyền
            //if (currentRole != "ADMIN" && user.Username != currentUser)
            //    return Forbid();

            // 🔥 Update field (KHÔNG update password ở đây nếu không cần)
            user.Username = updatedUser.Username ?? user.Username;
            user.Email = updatedUser.Email ?? user.Email;

            // 🔥 Chỉ ADMIN mới được đổi role
            //if (currentRole == "ADMIN" && !string.IsNullOrEmpty(updatedUser.Role))
            //{
            //    user.Role = updatedUser.Role.ToUpper();
            //}

            // 🔥 Nếu có đổi password thì hash lại
            if (!string.IsNullOrEmpty(updatedUser.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.Role
            });
        }
    }
}