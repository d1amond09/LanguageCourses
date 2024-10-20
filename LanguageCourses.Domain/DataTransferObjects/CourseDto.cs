namespace LanguageCourses.Domain.DataTransferObjects;

public record CourseDto
{
	public Guid CourseId { get; set; }

	public Guid EmployeeId { get; set; }

	public string Name { get; set; } = null!;

	public string TrainingProgram { get; set; } = null!;

	public string? Description { get; set; }

	public string Intensity { get; set; } = null!;

	public int GroupSize { get; set; }

	public int AvailableSeats { get; set; }

	public int Hours { get; set; }

	public decimal TuitionFee { get; set; }
}
