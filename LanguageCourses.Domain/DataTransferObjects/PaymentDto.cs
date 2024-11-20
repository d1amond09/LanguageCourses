namespace LanguageCourses.Domain.DataTransferObjects;

public record PaymentDto
{
	public Guid PaymentId { get; init; }

	public DateOnly Date { get; init; }

	public string? Purpose { get; init; }

	public decimal Amount { get; init; }

	public Guid StudentId { get; init; }
}
