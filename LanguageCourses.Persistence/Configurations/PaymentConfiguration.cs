using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageCourses.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasData(
            new Payment
            {
                PaymentId = Guid.Parse("99999999-9999-9999-9999-999999999992"),
                Date = DateOnly.FromDateTime(DateTime.Now),
                Purpose = "Оплата курса Основы программирования",
                Amount = 15000m,
                StudentId = Guid.Parse("99999999-9999-9999-9999-999999999993") 
            },
            new Payment
            {
                PaymentId = Guid.Parse("99999999-9999-9999-9999-999999999994"),
                Date = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)),
                Purpose = "Оплата курса Разработка веб-приложений",
                Amount = 20000m,
                StudentId = Guid.Parse("99999999-9999-9999-9999-999999999995") 
            }
        );
    }
}