using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageCourses.Persistence.Configurations;

public class JobTitleConfiguration : IEntityTypeConfiguration<JobTitle>
{
    public void Configure(EntityTypeBuilder<JobTitle> builder)
    {
        builder.HasData(
            new JobTitle
            {
                JobTitleId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Name = "Преподаватель",
                Salary = 60000m,
                Responsibilities = "Преподавание курсов.",
                Requirements = "Высшее образование."
            },
            new JobTitle
            {
                JobTitleId = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                Name = "Администратор",
                Salary = 40000m,
                Responsibilities = "Организация учебного процесса.",
                Requirements = "Опыт работы в образовании."
            }
        );
    }
}