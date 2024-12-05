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

        modelBuilder.Entity<Student>()
        .HasMany(c => c.Courses)
        .WithMany(s => s.Students)
        .UsingEntity<Dictionary<string, string>>(
            "CourseStudents",
            j => j
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey("CourseID"),
            j => j
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey("StudentID"),
            j =>
            {
                j.HasKey("CourseID", "StudentID");
            });
    }
}
