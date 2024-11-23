using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Domain.DataTransferObjects;

public record PaymentDto
{
    public Guid Id { get; init; }
    public Guid StudentId { get; init; }
    public DateOnly Date { get; init; }
    public string? Purpose { get; init; }
    public decimal Amount { get; init; }
    public StudentDto? Student { get; init; }
}

public record PaymentForManipulationDto
{
    public DateOnly Date { get; init; }
    public string? Purpose { get; init; }
    public decimal Amount { get; init; }
    public Guid StudentId { get; init; }
    public StudentDto? Student { get; init; }
}

public record PaymentForUpdateDto : PaymentForManipulationDto;
public record PaymentForCreationDto : PaymentForManipulationDto;