namespace LanguageCourses.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string? Name { get; set; }
    public string? TrainingProgram { get; set; }
    public string? Description { get; set; }
    public string? Intensity { get; set; }
    public int GroupSize { get; set; }
    public int AvailableSeats { get; set; }
    public int Hours { get; set; }
    public double TuitionFee { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual ICollection<Student> Students { get; set; } = [];
}
