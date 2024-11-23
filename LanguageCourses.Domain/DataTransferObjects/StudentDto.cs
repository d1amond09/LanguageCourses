using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Domain.DataTransferObjects;

public record StudentDto
{
    public Guid Id { get; init; }
    public string? FullName { get; init; }
    public DateOnly BirthDate { get; init; }
    public string? Address { get; init; }
    public string? Phone { get; init; }
    public string? PassportNumber { get; init; }
    public ICollection<PaymentDto>? Payments { get; init; }
}

public record StudentForManipulationDto
{
    public string? FullName { get; init; }
    public DateOnly BirthDate { get; init; }
    public string? Address { get; init; }
    public string? Phone { get; init; }
    public string? PassportNumber { get; init; }
    public ICollection<PaymentDto>? Payments { get; init; }
}

public record StudentForUpdateDto : StudentForManipulationDto;
public record StudentForCreationDto : StudentForManipulationDto;