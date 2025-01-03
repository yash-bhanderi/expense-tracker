using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeCommandos.Domain.Dtos.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("User");

        entity.Property(e => e.UserId).HasColumnName("UserId");
        entity.Property(e => e.Username).HasMaxLength(100).IsRequired();
        entity.Property(e => e.Email).HasMaxLength(150).IsRequired();
        entity.Property(e => e.PasswordHash).HasMaxLength(255);
        entity.Property(e => e.IsActive).IsRequired();
        entity.Property(e => e.CreatedAt).IsRequired();

        entity.Property(e => e.GoogleId).HasMaxLength(100);
    }
}