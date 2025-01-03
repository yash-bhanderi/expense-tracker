using CodeCommandos.Domain;
using CodeCommandos.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeCommandos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseTagsController : ControllerBase
{
    private readonly ExpenseTrackingContext _context;

    public ExpenseTagsController(ExpenseTrackingContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpenseTag([FromBody] ExpenseTag tag)
    {
        _context.ExpenseTags.Add(tag);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTagsForExpense), new { id = tag.ExpenseTagId }, tag);
    }

    [HttpGet("expense/{expenseId}")]
    public async Task<IActionResult> GetTagsForExpense(long expenseId)
    {
        var tags = await _context.ExpenseTags
            .Where(t => t.ExpenseId == expenseId)
            .ToListAsync();
        return Ok(tags);
    }
}
