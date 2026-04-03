using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderItemController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetAll()
    {
        return Ok(await _context.OrderItems.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<OrderItem>> Create(OrderItem item)
    {
        _context.OrderItems.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }
}