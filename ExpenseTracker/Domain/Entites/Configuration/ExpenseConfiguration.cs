using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeCommandos.Domain.Dtos.Configuration;

// Expense Configuration
public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> entity)
    {
        entity.ToTable("Expense");

        entity.Property(e => e.ExpenseId).HasColumnName("ExpenseId");
        entity.Property(e => e.Description).HasMaxLength(255);
        entity.Property(e => e.Amount).HasColumnType("decimal(18,2)").IsRequired();
        entity.Property(e => e.ExpenseDate).IsRequired();
        entity.Property(e => e.CreatedAt).IsRequired();

        entity.HasOne(e => e.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(e => e.UserId);

        entity.HasOne(e => e.Category)
            .WithMany(c => c.Expenses)
            .HasForeignKey(e => e.CategoryId);
    }
}