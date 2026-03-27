using connetdb.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private static readonly List<Order> Orders = new List<Order>();

    [HttpGet]
    public IEnumerable<Order> GetAll() => Orders;

    [HttpGet("{id}")]
    public ActionResult<Order> GetById(int id)
    {
        var order = Orders.Find(o => o.Id == id);
        if (order == null) return NotFound();
        return order;
    }

    [HttpPost]
    public ActionResult<Order> Create(Order order)
    {
        Orders.Add(order);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Order updated)
    {
        var index = Orders.FindIndex(o => o.Id == id);
        if (index == -1) return NotFound();
        Orders[index] = updated;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var order = Orders.Find(o => o.Id == id);
        if (order == null) return NotFound();
        Orders.Remove(order);
        return NoContent();
    }
}