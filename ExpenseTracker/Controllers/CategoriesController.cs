using CodeCommandos.Domain;
using CodeCommandos.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeCommandos.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ExpenseTrackingContext _context;

    public CategoriesController(ExpenseTrackingContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(long id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return Ok(category);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetCategoriesForUser(long userId)
    {
        var categories = await _context.Categories.Where(c => c.UserId == userId).ToListAsync();
        return Ok(categories);
    }
}
