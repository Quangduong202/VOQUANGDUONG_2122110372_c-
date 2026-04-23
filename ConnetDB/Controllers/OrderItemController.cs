using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class OrderDetailController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderDetailController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDetail>>> GetAll()
    {
        return Ok(await _context.OrderDetails.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<OrderDetail>> Create(OrderDetail item)
    {
        _context.OrderDetails.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }
}