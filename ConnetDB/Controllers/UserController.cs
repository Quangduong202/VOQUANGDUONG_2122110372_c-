using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConnetDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.Username,
                    u.Role,
                    u.Avatar,
                    u.Phone
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return Ok(new
            {
                user.Id,
                user.Email,
                user.   Username,
                user.Role,
                user.Avatar,
                user.Phone
            });
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            // validate email
            if (string.IsNullOrEmpty(user.Email))
                return BadRequest("Email không được để trống");

            // check email tồn tại
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest("Email đã tồn tại");

            // validate phone INT
            if (user.Phone <= 0)
                return BadRequest("Số điện thoại không hợp lệ");

            // validate password
            if (string.IsNullOrEmpty(user.Password))
                return BadRequest("Password không được để trống");

            // hash password
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new
            {
                user.Id,
                user.Email,
                user.Username,
                user.Role,
                user.Phone
            });
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            // check email trùng (trừ chính nó)
            if (!string.IsNullOrEmpty(updatedUser.Email))
            {
                var exist = await _context.Users
                    .AnyAsync(u => u.Email == updatedUser.Email && u.Id != id);

                if (exist)
                    return BadRequest("Email đã tồn tại");

                user.Email = updatedUser.Email;
            }

            if (!string.IsNullOrEmpty(updatedUser.Username))
                user.Username = updatedUser.Username;

            if (!string.IsNullOrEmpty(updatedUser.Avatar))
                user.Avatar = updatedUser.Avatar;

            // update phone INT
            if (updatedUser.Phone > 0)
                user.Phone = updatedUser.Phone;

            // update role
            user.Role = updatedUser.Role;

            // update password nếu có
            if (!string.IsNullOrEmpty(updatedUser.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Email,
                user.Username,
                user.Role,
                user.Phone
            });
        }

        // DELETE
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
    }
}