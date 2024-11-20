namespace LanguageCourses.Domain.Entities;

public class JobTitle
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public double Salary { get; set; }

    public string? Responsibilities { get; set; }

    public string? Requirements { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = [];
}
