using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Persistence.Configurations;

public class JobTitleConfiguration : IEntityTypeConfiguration<JobTitle>
{
    public void Configure(EntityTypeBuilder<JobTitle> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("JobTitleID");

        builder.Property(e => e.Salary)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");

        builder.HasData
        (
            new JobTitle
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-111111111111"),
                Salary = 3000,
                Name = "Преподаватель",
                Requirements = "",
                Responsibilities = "",
            }
        );
    }
}
