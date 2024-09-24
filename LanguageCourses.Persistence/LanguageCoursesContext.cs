using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Domain;

public partial class LanguageCoursesContext : DbContext
{
    public LanguageCoursesContext()
    {
    }

    public LanguageCoursesContext(DbContextOptions<LanguageCoursesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CoursesStudent> CoursesStudents { get; set; }

    public virtual DbSet<Debtor> Debtors { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=D1AMOND;Database=LanguageCourses;Integrated Security=true; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D7187E3190DCB");

            entity.Property(e => e.CourseId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CourseID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
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
        });

        modelBuilder.Entity<CoursesStudent>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Course).WithMany()
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__CoursesSt__Cours__627A95E8");

            entity.HasOne(d => d.Student).WithMany()
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__CoursesSt__Stude__636EBA21");
        });

        modelBuilder.Entity<Debtor>(entity =>
        {
            entity.HasKey(e => e.DebtorId).HasName("PK__Debtors__C47716E6B0A48EEA");

            entity.Property(e => e.DebtorId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("DebtorID");
            entity.Property(e => e.DebtAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Student).WithMany(p => p.Debtors)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Debtors__Student__758D6A5C");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1D2ECEA86");

            entity.HasIndex(e => e.PassportNumber, "UQ__Employee__45809E71FECC59B5").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__Employee__5C7E359E763507D9").IsUnique();

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
                .HasConstraintName("FK__Employees__JobTi__6CF8245B");
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.HasKey(e => e.JobTitleId).HasName("PK__JobTitle__35382FC918C34FC8");

            entity.Property(e => e.JobTitleId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("JobTitleID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Requirements)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Responsibilities)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58D200545B");

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
                .HasConstraintName("FK__Payments__Studen__70C8B53F");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A791123D2BB");

            entity.HasIndex(e => e.PassportNumber, "UQ__Students__45809E716551E45A").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__Students__5C7E359E53246B35").IsUnique();

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
