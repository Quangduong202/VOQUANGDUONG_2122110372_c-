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
        public CategoryController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
            => Ok(await _context.Categories.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var data = await _context.Categories.FindAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            _context.Categories.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category updated)
        {
            var data = await _context.Categories.FindAsync(id);
            if (data == null) return NotFound();

            data.Name = updated.Name;
            data.Image = updated.Image;

            await _context.SaveChangesAsync();
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Categories.FindAsync(id);
            if (data == null) return NotFound();

            _context.Categories.Remove(data);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}