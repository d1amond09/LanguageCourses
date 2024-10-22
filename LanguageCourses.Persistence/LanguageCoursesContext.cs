using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence;

public partial class LanguageCoursesContext(DbContextOptions<LanguageCoursesContext> options) : DbContext(options)
{
	public virtual DbSet<Course> Courses { get; set; }

	public virtual DbSet<Employee> Employees { get; set; }

	public virtual DbSet<JobTitle> JobTitles { get; set; }

	public virtual DbSet<Payment> Payments { get; set; }

	public virtual DbSet<Student> Students { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Course>(entity =>
		{
			entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71876B88FF34");

			entity.Property(e => e.CourseId)
				.HasDefaultValueSql("(newid())")
				.HasColumnName("CourseID");
			entity.Property(e => e.Description)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
			entity.Property(e => e.Intensity)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.Name)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.TrainingProgram)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.TuitionFee).HasColumnType("decimal(10, 2)");

			entity.HasOne(d => d.Employee).WithMany(p => p.Courses)
				.HasForeignKey(d => d.EmployeeId)
				.HasConstraintName("FK__Courses__Employe__3F1C4B12");

			entity.HasMany(d => d.Students).WithMany(p => p.Courses)
				.UsingEntity<Dictionary<string, object>>(
					"CourseStudent",
					r => r.HasOne<Student>().WithMany()
						.HasForeignKey("StudentId")
						.HasConstraintName("FK__CourseStu__Stude__4B8221F7"),
					l => l.HasOne<Course>().WithMany()
						.HasForeignKey("CourseId")
						.HasConstraintName("FK__CourseStu__Cours__4A8DFDBE"),
					j =>
					{
						j.HasKey("CourseId", "StudentId").HasName("PK__CourseSt__4A0123205C2964B0");
						j.ToTable("CourseStudents");
						j.IndexerProperty<Guid>("CourseId").HasColumnName("CourseID");
						j.IndexerProperty<Guid>("StudentId").HasColumnName("StudentID");
					});
		});

		modelBuilder.Entity<Employee>(entity =>
		{
			entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF14A0267E9");

			entity.HasIndex(e => e.PassportNumber, "UQ__Employee__45809E71543558D3").IsUnique();

			entity.HasIndex(e => e.Phone, "UQ__Employee__5C7E359EB6DB6BF9").IsUnique();

			entity.Property(e => e.EmployeeId)
				.HasDefaultValueSql("(newid())")
				.HasColumnName("EmployeeID");
			entity.Property(e => e.Address)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.Education)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");
			entity.Property(e => e.Midname)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.Name)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.PassportNumber)
				.HasMaxLength(9)
				.IsUnicode(false);
			entity.Property(e => e.Phone)
				.HasMaxLength(20)
				.IsUnicode(false);
			entity.Property(e => e.Surname)
				.HasMaxLength(255)
				.IsUnicode(false);

			entity.HasOne(d => d.JobTitle).WithMany(p => p.Employees)
				.HasForeignKey(d => d.JobTitleId)
				.HasConstraintName("FK__Employees__JobTi__3B4BBA2E");
		});

		modelBuilder.Entity<JobTitle>(entity =>
		{
			entity.HasKey(e => e.JobTitleId).HasName("PK__JobTitle__35382FC9A56AE929");

			entity.Property(e => e.JobTitleId)
				.HasDefaultValueSql("(newid())")
				.HasColumnName("JobTitleID");
			entity.Property(e => e.Name)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.Requirements)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.Responsibilities)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
		});

		modelBuilder.Entity<Payment>(entity =>
		{
			entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A5874FA56DF");

			entity.Property(e => e.PaymentId)
				.HasDefaultValueSql("(newid())")
				.HasColumnName("PaymentID");
			entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
			entity.Property(e => e.Purpose)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.StudentId).HasColumnName("StudentID");

			entity.HasOne(d => d.Student).WithMany(p => p.Payments)
				.HasForeignKey(d => d.StudentId)
				.HasConstraintName("FK__Payments__Studen__4F52B2DB");
		});

		modelBuilder.Entity<Student>(entity =>
		{
			entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A7968AC35E8");

			entity.HasIndex(e => e.PassportNumber, "UQ__Students__45809E717E8906CA").IsUnique();

			entity.HasIndex(e => e.Phone, "UQ__Students__5C7E359EED8438DE").IsUnique();

			entity.Property(e => e.StudentId)
				.HasDefaultValueSql("(newid())")
				.HasColumnName("StudentID");
			entity.Property(e => e.Address)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.MidName)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.Name)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.PassportNumber)
				.HasMaxLength(9)
				.IsUnicode(false);
			entity.Property(e => e.Phone)
				.HasMaxLength(20)
				.IsUnicode(false);
			entity.Property(e => e.Surname)
				.HasMaxLength(255)
				.IsUnicode(false);
		});


		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
