using connetdb.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private static readonly List<Payment> Payments = new List<Payment>();

    [HttpGet]
    public IEnumerable<Payment> GetAll() => Payments;

    [HttpGet("{id}")]
    public ActionResult<Payment> GetById(int id)
    {
        var payment = Payments.Find(p => p.Id == id);
        if (payment == null) return NotFound();
        return payment;
    }

    [HttpPost]
    public ActionResult<Payment> Create(Payment payment)
    {
        Payments.Add(payment);
        return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Payment updated)
    {
        var index = Payments.FindIndex(p => p.Id == id);
        if (index == -1) return NotFound();
        Payments[index] = updated;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var payment = Payments.Find(p => p.Id == id);
        if (payment == null) return NotFound();
        Payments.Remove(payment);
        return NoContent();
    }
}