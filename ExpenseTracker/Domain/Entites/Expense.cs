namespace CodeCommandos.Domain.Dtos;

public class Expense
{
    public long ExpenseId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    public long UserId { get; set; }
    public User User { get; set; }

    public long CategoryId { get; set; }
    public Category Category { get; set; }

    // Navigation Properties
    public ICollection<ExpenseTag> Tags { get; set; }
}
