using Microsoft.AspNetCore.Mvc;

namespace CodeCommandos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly ExpenseTrackingContext _context;

    public ExpensesController(ExpenseTrackingContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetExpense), new { id = expense.ExpenseId }, expense);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(long id)
    {
        var expense = await _context.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.ExpenseId == id);
        if (expense == null) return NotFound();
        return Ok(expense);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetExpensesForUser(long userId)
    {
        var expenses = await _context.Expenses
            .Where(e => e.UserId == userId)
            .Include(e => e.Category)
            .ToListAsync();
        return Ok(expenses);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(long id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null) return NotFound();

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
