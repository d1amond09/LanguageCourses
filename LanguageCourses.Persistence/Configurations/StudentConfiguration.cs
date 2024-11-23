using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("StudentID");

        builder.HasData
        (
            new Student
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a1111871"),
                Surname = "Ивашко",
                Name = "Вадим",
                MidName = "Михайлович",
                BirthDate = DateOnly.FromDateTime(DateTime.Now),
                Address = "г. Гомель, ул. Героев-Подпольщиков, д. 21, кв. 116",
                PassportNumber = "HB3331134",
                Phone = "+375445461239",
            }
        );
    }
}
