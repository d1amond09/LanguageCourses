using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Domain.DataTransferObjects;

public record JobTitleDto
{
	public Guid JobTitleId { get; set; }

	public string Name { get; set; } = null!;

	public decimal Salary { get; set; }

	public string? Responsibilities { get; set; }

	public string? Requirements { get; set; }
    public ICollection<EmployeeDto>? Employees { get; set; } 
}
