using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Domain.DataTransferObjects;

public record EmployeeDto
{
	public Guid EmployeeId { get; set; }

	public Guid JobTitleId { get; set; }

	public string? Surname { get; set; }

	public string? Name { get; set; } 

	public string? Midname { get; set; }

    public DateOnly BirthDate { get; set; }

	public string? Address { get; set; }

	public string? Phone { get; set; }

	public string? PassportNumber { get; set; } 

	public string? Education { get; set; } 
    public ICollection<CourseDto>? Courses { get; set; }
    public JobTitleDto? JobTitle { get; set; }

    public override string ToString()
    {
        return $"{Surname} {Name} {Midname}";
    }
}
