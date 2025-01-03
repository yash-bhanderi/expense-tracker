namespace CodeCommandos.Domain.Dtos;

public class Category
{
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign Key
    public long UserId { get; set; }
    public User User { get; set; }

    // Navigation Properties
    public ICollection<Expense> Expenses { get; set; }
}
