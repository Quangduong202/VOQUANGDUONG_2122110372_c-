using connetdb.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private static readonly List<Review> Reviews = new List<Review>();

    [HttpGet]
    public IEnumerable<Review> GetAll() => Reviews;

    [HttpGet("{id}")]
    public ActionResult<Review> GetById(int id)
    {
        var review = Reviews.Find(r => r.Id == id);
        if (review == null) return NotFound();
        return review;
    }

    [HttpPost]
    public ActionResult<Review> Create(Review review)
    {
        Reviews.Add(review);
        return CreatedAtAction(nameof(GetById), new { id = review.Id }, review);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Review updated)
    {
        var index = Reviews.FindIndex(r => r.Id == id);
        if (index == -1) return NotFound();
        Reviews[index] = updated;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var review = Reviews.Find(r => r.Id == id);
        if (review == null) return NotFound();
        Reviews.Remove(review);
        return NoContent();
    }
}