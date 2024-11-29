using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageCourses.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("EmployeeID");

        builder.HasData
        (
            new Employee
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"),
                Surname = "Бердников",
                Name = "Максим",
                Midname = "Михайлович",
                BirthDate = DateOnly.FromDateTime(DateTime.Now),
                Address = "г. Гомель, ул. Героев-Подпольщиков, д. 23, кв. 146",
                Education = "Высшее",
                PassportNumber = "HB3636156",
                Phone = "+375445464319",
                JobTitleId = new Guid("c9d4c053-49b6-410c-bc78-111111111111")
            }
        );
    }
}
