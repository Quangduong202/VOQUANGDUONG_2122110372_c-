using connetdb.Data;
using connetdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReviewController(AppDbContext context)
    {
        _context = context;
    }

    // Lấy tất cả review
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetAll()
    {
        return Ok(await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Product)
            .ToListAsync());
    }

    // Tạo review mới
    [HttpPost]
    public async Task<ActionResult<Review>> Create(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return Ok(review);
    }

    // Xóa review theo Id
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound(new { message = "Review not found" });
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Review deleted successfully" });
    }

    // Sửa review theo Id
    [HttpPut("{id}")]
    public async Task<ActionResult<Review>> Update(int id, Review updatedReview)
    {
        if (id != updatedReview.Id)
        {
            return BadRequest(new { message = "ID mismatch" });
        }

        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound(new { message = "Review not found" });
        }

        // Cập nhật các trường
        review.UserId = updatedReview.UserId;
        review.ProductId = updatedReview.ProductId;
        review.Rating = updatedReview.Rating;
        review.Comment = updatedReview.Comment;
        // CreatedAt thường không sửa khi update

        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();

        return Ok(review);
    }
}