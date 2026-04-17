using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConnetDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        // ✅ API cần đăng nhập
        [Authorize]
        [HttpGet("user")]
        public IActionResult User()
        {
            return Ok("Bạn đã đăng nhập");
        }

        // ✅ API chỉ Admin
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return Ok("Chỉ Admin mới vào được");
        }

        // ✅ API public (không cần login)
        [AllowAnonymous]
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok("Ai cũng vào được");
        }
    }
}