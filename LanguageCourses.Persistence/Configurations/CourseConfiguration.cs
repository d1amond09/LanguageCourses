using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageCourses.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasData(
            new Course
            {
                CourseId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                EmployeeId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Основы программирования",
                TrainingProgram = "Изучение основ языков программирования",
                Description = "Курс для начинающих программистов",
                Intensity = "Средняя",
                GroupSize = 15,
                AvailableSeats = 10,
                Hours = 40,
                TuitionFee = 15000m
            },
            new Course
            {
                CourseId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                EmployeeId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Разработка веб-приложений",
                TrainingProgram = "Создание интерактивных веб-приложений с использованием JavaScript и других технологий",
                Description = "Курс для веб-разработчиков",
                Intensity = "Высокая",
                GroupSize = 12,
                AvailableSeats = 8,
                Hours = 60,
                TuitionFee = 20000m
            }

        );
    }
}