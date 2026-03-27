using connetdb.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class OrderItemController : ControllerBase
{
    private static readonly List<OrderItem> OrderItems = new List<OrderItem>();

    [HttpGet]
    public IEnumerable<OrderItem> GetAll() => OrderItems;

    [HttpGet("{id}")]
    public ActionResult<OrderItem> GetById(int id)
    {
        var item = OrderItems.Find(oi => oi.Id == id);
        if (item == null) return NotFound();
        return item;
    }

    [HttpPost]
    public ActionResult<OrderItem> Create(OrderItem item)
    {
        OrderItems.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, OrderItem updated)
    {
        var index = OrderItems.FindIndex(oi => oi.Id == id);
        if (index == -1) return NotFound();
        OrderItems[index] = updated;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = OrderItems.Find(oi => oi.Id == id);
        if (item == null) return NotFound();
        OrderItems.Remove(item);
        return NoContent();
    }
}