namespace LanguageCourses.Domain.DataTransferObjects;

public record JobTitleDto
{
	public Guid JobTitleId { get; init; }

	public string? Name { get; init; }

	public decimal Salary { get; init; }

	public string? Responsibilities { get; init; }

	public string? Requirements { get; init; }
}
