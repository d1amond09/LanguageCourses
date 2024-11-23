namespace LanguageCourses.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public Guid JobTitleId { get; set; }
    public string? Surname { get; set; }
    public string? Name { get; set; }
    public string? Midname { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? PassportNumber { get; set; } 
    public string? Education { get; set; } 
    public virtual ICollection<Course> Courses { get; set; } = [];
    public virtual JobTitle? JobTitle { get; set; }
}
