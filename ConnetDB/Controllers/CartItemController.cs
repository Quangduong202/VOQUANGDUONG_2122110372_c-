using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class CartItemController : ControllerBase
{
    private readonly AppDbContext _context;

    public CartItemController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartItem>>> GetAll()
    {
        return Ok(await _context.CartItems.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<CartItem>> Create(CartItem item)
    {
        _context.CartItems.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.CartItems.FindAsync(id);
        if (item == null) return NotFound();

        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}