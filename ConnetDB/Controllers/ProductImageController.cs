using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ProductImageController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductImageController(AppDbContext context)
    {
        _context = context;
    }

    // GET ALL
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductImage>>> GetAll()
    {
        return Ok(await _context.ProductImages
            .Include(p => p.Product)
            .ToListAsync());
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductImage>> GetById(int id)
    {
        var data = await _context.ProductImages
            .Include(p => p.Product)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (data == null) return NotFound();
        return Ok(data);
    }

    // 🔥 GET BY PRODUCT ID (quan trọng)
    [HttpGet("product/{productId}")]
    public async Task<ActionResult<IEnumerable<ProductImage>>> GetByProduct(int productId)
    {
        var data = await _context.ProductImages
            .Where(p => p.ProductId == productId)
            .ToListAsync();

        return Ok(data);
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult<ProductImage>> Create(ProductImage model)
    {
        // check FK
        if (!await _context.Products.AnyAsync(p => p.Id == model.ProductId))
            return BadRequest("Product không tồn tại");

        _context.ProductImages.Add(model);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductImage updated)
    {
        var data = await _context.ProductImages.FindAsync(id);
        if (data == null) return NotFound("Không tìm thấy ảnh");

        // check FK
        if (!await _context.Products.AnyAsync(p => p.Id == updated.ProductId))
            return BadRequest("Product không tồn tại");

        data.ProductId = updated.ProductId;
        data.ImageUrl = updated.ImageUrl;

        await _context.SaveChangesAsync();

        return Ok(data);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var data = await _context.ProductImages.FindAsync(id);
        if (data == null) return NotFound();

        _context.ProductImages.Remove(data);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}