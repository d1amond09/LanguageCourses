namespace LanguageCourses.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public string? Purpose { get; set; }
    public double Amount { get; set; }
    public Guid StudentId { get; set; }
    public virtual Student? Student { get; set; } 
}
