namespace LanguageCourses.Domain.DataTransferObjects;

public record CourseDto
{
    public Guid Id { get; init; }
    public Guid EmployeeId { get; init; }
    public EmployeeDto? Employee { get; init; }
    public string? Name { get; init; }

    public string? TrainingProgram { get; init; }

    public string? Description { get; init; }

    public string? Intensity { get; init; }

    public int GroupSize { get; init; }

    public int AvailableSeats { get; init; }

    public int Hours { get; init; }

    public decimal TuitionFee { get; init; }
}

public record CourseForManipulationDto
{
    public EmployeeDto? Employee { get; init; }
    public string? Name { get; init; }
    public string? TrainingProgram { get; init; }
    public string? Description { get; init; }
    public string? Intensity { get; init; }
    public int GroupSize { get; init; }
    public int AvailableSeats { get; init; }
    public int Hours { get; init; }
    public decimal TuitionFee { get; init; }
}

public record CourseForUpdateDto : CourseForManipulationDto;
public record CourseForCreationDto : CourseForManipulationDto;