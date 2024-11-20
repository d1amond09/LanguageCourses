namespace LanguageCourses.Domain.Entities;

public class Student
{
    public Guid Id { get; set; } 

    public string? Surname { get; set; } 

    public string? Name { get; set; } 

    public string? MidName { get; set; }

    public DateOnly BirthDate { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; } 

    public string? PassportNumber { get; set; } 

    public virtual ICollection<Payment> Payments { get; set; } = [];

    public virtual ICollection<Course> Courses { get; set; } = [];
}
