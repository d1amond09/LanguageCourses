namespace LanguageCourses.Domain.DataTransferObjects;

public record EmployeeDto
{
	public Guid EmployeeId { get; set; }

	public Guid JobTitleId { get; set; }

	public string Surname { get; set; } = null!;

	public string Name { get; set; } = null!;

	public string? Midname { get; set; }

	public DateOnly BirthDate { get; set; }

	public string? Address { get; set; }

	public string? Phone { get; set; }

	public string PassportNumber { get; set; } = null!;

	public string Education { get; set; } = null!;
}
