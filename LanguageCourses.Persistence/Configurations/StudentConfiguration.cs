using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasData(
            new Student
            {
                StudentId = Guid.Parse("99999999-9999-9999-9999-999999999993"),
                Surname = "Смирнов",
                Name = "Алексей",
                MidName = "Иванович",
                BirthDate = new DateOnly(2000, 5, 15),
                Address = "Москва, ул. 3",
                Phone = "+79011112233",
                PassportNumber = "HB6543122",
            },
            new Student
            {
                StudentId = Guid.Parse("99999999-9999-9999-9999-999999999995"),
                Surname = "Попов",
                Name = "Сергей",
                MidName = null,
                BirthDate = new DateOnly(1999, 7, 20),
                Address = "Санкт-Петербург, ул. 4",
                Phone = "+79044445556",
                PassportNumber = "HB6543121",
            }
        );
    }
}