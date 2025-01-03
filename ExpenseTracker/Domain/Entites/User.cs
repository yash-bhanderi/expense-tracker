namespace CodeCommandos.Domain.Dtos;

public class User
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // OAuth Fields
    public string GoogleId { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Expense> Expenses { get; set; }
}
