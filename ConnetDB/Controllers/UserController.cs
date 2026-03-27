using connetdb.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private static readonly List<User> Users = new List<User>();

    [HttpGet]
    public IEnumerable<User> GetAll() => Users;

    [HttpGet("{id}")]
    public ActionResult<User> GetById(int id)
    {
        var user = Users.Find(u => u.Id == id);
        if (user == null) return NotFound();
        return user;
    }

    [HttpPost]
    public ActionResult<User> Create(User user)
    {
        Users.Add(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, User updated)
    {
        var index = Users.FindIndex(u => u.Id == id);
        if (index == -1) return NotFound();
        Users[index] = updated;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = Users.Find(u => u.Id == id);
        if (user == null) return NotFound();
        Users.Remove(user);
        return NoContent();
    }
}