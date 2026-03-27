using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnetDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET /Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _context.Products
                                         .Include(p => p.Category) // nếu muốn load category info
                                         .ToListAsync();
            return Ok(products);
        }

        // GET /Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST /Product
        [HttpPost]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync(); // 🔑 ghi vào SQL

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT /Product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updated)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            // Update các trường
            product.Name = updated.Name;
            product.Description = updated.Description;
            product.Price = updated.Price;
            product.Stock = updated.Stock;
            product.CategoryId = updated.CategoryId;
            product.CreatedAt = updated.CreatedAt;

            await _context.SaveChangesAsync(); // 🔑 commit vào SQL
            return NoContent();
        }

        // DELETE /Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(); // 🔑 commit vào SQL
            return NoContent();
        }
    }
}