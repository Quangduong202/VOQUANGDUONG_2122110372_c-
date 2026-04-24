using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    // GET ALL
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        return Ok(await _context.Products
           
            .ToListAsync());
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _context.Products
         
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return NotFound();
        return Ok(product);
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        // check FK
        if (!await _context.Categories.AnyAsync(c => c.Id == product.CategoryId))
            return BadRequest("Category không tồn tại");

        if (!await _context.Brands.AnyAsync(b => b.Id == product.BrandId))
            return BadRequest("Brand không tồn tại");

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product updated)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound("Product không tồn tại");

        // check FK
        if (!await _context.Categories.AnyAsync(c => c.Id == updated.CategoryId))
            return BadRequest("Category không tồn tại");

        if (!await _context.Brands.AnyAsync(b => b.Id == updated.BrandId))
            return BadRequest("Brand không tồn tại");

        // update FULL theo bảng
        product.Name = updated.Name;
        product.Image = updated.Image;
        product.Price = updated.Price;
        product.OldPrice = updated.OldPrice;
        product.Description = updated.Description;
        product.Specification = updated.Specification;
        product.BuyTurn = updated.BuyTurn;
        product.Quantity = updated.Quantity;
        product.CategoryId = updated.CategoryId;
        product.BrandId = updated.BrandId;

        await _context.SaveChangesAsync();

        return Ok(product);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}