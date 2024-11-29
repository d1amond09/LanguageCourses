using LanguageCourses.Domain.Entities;
using LanguageCourses.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence;

public class LanguageCoursesContext(DbContextOptions<LanguageCoursesContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<JobTitle> JobTitles { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new JobTitleConfiguration());

        modelBuilder.Entity<Course>()
        .HasMany(c => c.Students)
        .WithMany(s => s.Courses)
        .UsingEntity<Dictionary<string, object>>(
            "CourseStudents",
            j => j
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey("StudentID"),
            j => j
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey("CourseID"),
            j =>
            {
                j.HasKey("CourseID", "StudentID");
            });
    }
}
