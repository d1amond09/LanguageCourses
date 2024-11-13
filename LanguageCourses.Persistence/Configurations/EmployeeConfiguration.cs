using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageCourses.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasData(
            new Employee
            {
                EmployeeId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                JobTitleId = Guid.Parse("66666666-6666-6666-6666-666666666666"), 
                Surname = "Иванов",
                Name = "Иван",
                Midname = "Иванович",
                BirthDate = new DateOnly(1990, 1, 1),
                Address = "Москва, ул. 1",
                Phone = "+79012345678",
                PassportNumber = "HB6543129",
                Education = "Высшее"
            },
            new Employee
            {
                EmployeeId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                JobTitleId = Guid.Parse("88888888-8888-8888-8888-888888888888"), 
                Surname = "Петров",
                Name = "Петр",
                Midname = "Петрович",
                BirthDate = new DateOnly(1985, 2, 2),
                Address = "Санкт-Петербург, ул. 2",
                Phone = "+79087654321",
                PassportNumber = "HB6543111",
                Education = "Высшее"
            }
        );
    }
}