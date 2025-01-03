namespace CodeCommandos.Domain.Dtos;

public class ExpenseTag
{
    public long ExpenseTagId { get; set; }
    public string Tag { get; set; }

    // Foreign Key
    public long ExpenseId { get; set; }
    public Expense Expense { get; set; }
}
