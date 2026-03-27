using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace connetdb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET /Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        // GET /Category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        // POST /Category
        [HttpPost]
        public async Task<ActionResult<Category>> Create([FromBody] Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(); // 🔑 commit vào SQL

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        // PUT /Category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category updated)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            // Update các trường
            category.Name = updated.Name;
            category.Description = updated.Description;
            category.CreatedAt = updated.CreatedAt;

            await _context.SaveChangesAsync(); // 🔑 commit vào SQL
            return NoContent();
        }

        // DELETE /Category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(); // 🔑 commit vào SQL
            return NoContent();
        }
    }
}