using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly AppDbContext _context;

    public PaymentController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Payment>>> GetAll()
    {
        return Ok(await _context.Payments.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Payment>> Create(Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return Ok(payment);
    }
}