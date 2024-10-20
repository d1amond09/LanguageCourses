namespace LanguageCourses.Domain.Entities.DataTransferObjects;

public record JobTitleDto
{
	public Guid JobTitleId { get; set; }

	public string Name { get; set; } = null!;

	public decimal Salary { get; set; }

	public string? Responsibilities { get; set; }

	public string? Requirements { get; set; }
}
