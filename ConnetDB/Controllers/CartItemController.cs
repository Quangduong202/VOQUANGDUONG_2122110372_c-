using connetdb.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CartItemController : ControllerBase
{
    private static readonly List<CartItem> CartItems = new List<CartItem>();

    [HttpGet]
    public IEnumerable<CartItem> GetAll() => CartItems;

    [HttpGet("{id}")]
    public ActionResult<CartItem> GetById(int id)
    {
        var item = CartItems.Find(ci => ci.Id == id);
        if (item == null) return NotFound();
        return item;
    }

    [HttpPost]
    public ActionResult<CartItem> Create(CartItem item)
    {
        CartItems.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CartItem updated)
    {
        var index = CartItems.FindIndex(ci => ci.Id == id);
        if (index == -1) return NotFound();
        CartItems[index] = updated;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = CartItems.Find(ci => ci.Id == id);
        if (item == null) return NotFound();
        CartItems.Remove(item);
        return NoContent();
    }
}