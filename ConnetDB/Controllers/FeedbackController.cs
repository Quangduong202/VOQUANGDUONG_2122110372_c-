using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly AppDbContext _context;

    public FeedbackController(AppDbContext context)
    {
        _context = context;
    }

    // GET ALL
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetAll()
    {
        return Ok(await _context.Feedbacks
            .Include(f => f.User)
            .Include(f => f.Product)
            .ToListAsync());
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Feedback>> GetById(int id)
    {
        var data = await _context.Feedbacks
            .Include(f => f.User)
            .Include(f => f.Product)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (data == null) return NotFound();
        return Ok(data);
    }

    // 🔥 GET BY PRODUCT (rất cần)
    [HttpGet("product/{productId}")]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetByProduct(int productId)
    {
        var data = await _context.Feedbacks
            .Where(f => f.ProductId == productId)
            .Include(f => f.User)
            .ToListAsync();

        return Ok(data);
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult<Feedback>> Create(Feedback model)
    {
        // check FK
        if (!await _context.Products.AnyAsync(p => p.Id == model.ProductId))
            return BadRequest("Product không tồn tại");

        if (!await _context.Users.AnyAsync(u => u.Id == model.UserId))
            return BadRequest("User không tồn tại");

        _context.Feedbacks.Add(model);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<ActionResult<Feedback>> Update(int id, Feedback updated)
    {
        if (id != updated.Id)
            return BadRequest("ID không khớp");

        var data = await _context.Feedbacks.FindAsync(id);
        if (data == null)
            return NotFound("Feedback không tồn tại");

        // check FK
        if (!await _context.Products.AnyAsync(p => p.Id == updated.ProductId))
            return BadRequest("Product không tồn tại");

        if (!await _context.Users.AnyAsync(u => u.Id == updated.UserId))
            return BadRequest("User không tồn tại");

        // update đúng theo bảng
        data.ProductId = updated.ProductId;
        data.UserId = updated.UserId;
        data.Star = updated.Star;
        data.Content = updated.Content;

        await _context.SaveChangesAsync();

        return Ok(data);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var data = await _context.Feedbacks.FindAsync(id);
        if (data == null)
            return NotFound("Feedback không tồn tại");

        _context.Feedbacks.Remove(data);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}