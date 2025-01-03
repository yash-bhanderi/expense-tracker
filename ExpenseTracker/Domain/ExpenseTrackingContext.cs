using CodeCommandos.Domain.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CodeCommandos.Domain;

public class ExpenseTrackingContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseTag> ExpenseTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure User
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Categories)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Expenses)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);

        // Configure Category
        modelBuilder.Entity<Category>()
            .HasKey(c => c.CategoryId);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Expenses)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId);

        // Configure Expense
        modelBuilder.Entity<Expense>()
            .HasKey(e => e.ExpenseId);

        // Configure ExpenseTag
        modelBuilder.Entity<ExpenseTag>()
            .HasKey(et => et.ExpenseTagId);

        modelBuilder.Entity<ExpenseTag>()
            .HasOne(et => et.Expense)
            .WithMany(e => e.Tags)
            .HasForeignKey(et => et.ExpenseId);
    }
}
