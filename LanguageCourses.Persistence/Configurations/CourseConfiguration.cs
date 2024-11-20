using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LanguageCourses.Domain.Entities;
using System.Reflection.Emit;

namespace LanguageCourses.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(e => e.TuitionFee)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");

        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("CourseId");

        builder.HasData
        (
            new Course
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                Name = "Курс по английскому языку",
                AvailableSeats = 16,
                Hours = 64,
                TuitionFee = 949.99,
                GroupSize = 24,
                Intensity = "3 раза в неделю",
                Description = "",
                TrainingProgram = "С1 - Advanced",
                EmployeeId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871")
            }
        );
    }
}
