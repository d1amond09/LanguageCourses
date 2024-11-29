namespace LanguageCourses.Domain.DataTransferObjects;

public record JobTitleDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public double Salary { get; init; }
    public string? Responsibilities { get; init; }
    public string? Requirements { get; init; }
    public ICollection<EmployeeDto>? Employees { get; init; }
}

public record JobTitleForManipulationDto
{
    public string? Name { get; init; }
    public double Salary { get; init; }
    public string? Responsibilities { get; init; }
    public string? Requirements { get; init; }
    public ICollection<EmployeeDto>? Employees { get; init; }
}

public record JobTitleForUpdateDto : JobTitleForManipulationDto;
public record JobTitleForCreationDto : JobTitleForManipulationDto;
