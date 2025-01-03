using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeCommandos.Domain.Dtos.Configuration;

// ExpenseTag Configuration
public class ExpenseTagConfiguration : IEntityTypeConfiguration<ExpenseTag>
{
    public void Configure(EntityTypeBuilder<ExpenseTag> entity)
    {
        entity.ToTable("ExpenseTag");

        entity.Property(e => e.ExpenseTagId).HasColumnName("ExpenseTagId");
        entity.Property(e => e.Tag).HasMaxLength(50).IsRequired();

        entity.HasOne(e => e.Expense)
            .WithMany(e => e.Tags)
            .HasForeignKey(e => e.ExpenseId);
    }
}