namespace LanguageCourses.Domain.DataTransferObjects;

public record CourseDto
{
	public Guid CourseId { get; init; }

	public Guid EmployeeId { get; init; }

	public string? Name { get; init; }

	public string? TrainingProgram { get; init; }

	public string? Description { get; init; }

	public string? Intensity { get; init; } 

	public int GroupSize { get; init; }

	public int AvailableSeats { get; init; }

	public int Hours { get; init; }

	public decimal TuitionFee { get; init; }
}
