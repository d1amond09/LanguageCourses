namespace LanguageCourses.Domain.DataTransferObjects;

public record StudentDto
{
	public Guid StudentId { get; init; } 
	public string? FullName { get; init; } 

	public DateOnly BirthDate { get; init; }

	public string? Address { get; init; }

	public string? Phone { get; init; } 

	public string? PassportNumber { get; init; } 
}
