using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageCourses.Persistence.Configurations;

public class JobTitleConfiguration : IEntityTypeConfiguration<JobTitle>
{
    public void Configure(EntityTypeBuilder<JobTitle> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired()
            .HasDefaultValueSql("NEWID()")
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
