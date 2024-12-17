using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageCourses.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired()
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("PaymentID");

        builder.Property(e => e.Amount)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");

        builder.HasData
        (
            new Payment
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-111111111111"),
                Amount = 1200,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Purpose = "Академическая задолженность",
                StudentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a1111871"),
            }
        );
    }
}
