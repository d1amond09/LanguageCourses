namespace LanguageCourses.Domain.DataTransferObjects;

public record EmployeeDto
{
	public Guid EmployeeId { get; init; }

	public Guid JobTitleId { get; init; }

	public string? FullName { get; init; }

	public DateOnly BirthDate { get; init; }

	public string? Address { get; init; }

	public string? Phone { get; init; }

	public string? PassportNumber { get; init; } 

	public string? Education { get; init; } 
}
