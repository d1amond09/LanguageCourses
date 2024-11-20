using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Domain.DataTransferObjects;

public record EmployeeDto
{
    public Guid Id { get; init; }
    public Guid JobTitleId { get; init; }
    public JobTitle? JobTitle { get; init; }

    public string? FullName { get; init; }

    public DateOnly BirthDate { get; init; }

    public string? Address { get; init; }

    public string? Phone { get; init; }

    public string? PassportNumber { get; init; }

    public string? Education { get; init; }
}

public record EmployeeForManipulationDto
{
    public Guid JobTitleId { get; init; }
    public string? FullName { get; init; }
    public DateOnly BirthDate { get; init; }
    public string? Address { get; init; }
    public string? Phone { get; init; }
    public string? PassportNumber { get; init; }
    public string? Education { get; init; }
}

public record EmployeeForUpdateDto : EmployeeForManipulationDto;
public record EmployeeForCreationDto : EmployeeForManipulationDto;