using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ConnetDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BrandController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetAll()
            => Ok(await _context.Brands.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetById(int id)
        {
            var data = await _context.Brands.FindAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> Create(Brand model)
        {
            _context.Brands.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Brand updated)
        {
            var data = await _context.Brands.FindAsync(id);
            if (data == null) return NotFound();

            data.Username = updated.Username;
            data.Image = updated.Image;

            await _context.SaveChangesAsync();
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Brands.FindAsync(id);
            if (data == null) return NotFound();

            _context.Brands.Remove(data);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
