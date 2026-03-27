using connetdb.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CartsController : ControllerBase
{
    private static readonly List<Cart> Carts = new List<Cart>();

    [HttpGet]
    public IEnumerable<Cart> GetAll() => Carts;

    [HttpGet("{id}")]
    public ActionResult<Cart> GetById(int id)
    {
        var cart = Carts.Find(c => c.Id == id);
        if (cart == null) return NotFound();
        return cart;
    }

    [HttpPost]
    public ActionResult<Cart> Create(Cart cart)
    {
        Carts.Add(cart);
        return CreatedAtAction(nameof(GetById), new { id = cart.Id }, cart);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Cart updated)
    {
        var index = Carts.FindIndex(c => c.Id == id);
        if (index == -1) return NotFound();
        Carts[index] = updated;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var cart = Carts.Find(c => c.Id == id);
        if (cart == null) return NotFound();
        Carts.Remove(cart);
        return NoContent();
    }
}