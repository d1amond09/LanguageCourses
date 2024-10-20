namespace LanguageCourses.Domain.Entities.DataTransferObjects;

public record PaymentDto
{
	public Guid PaymentId { get; set; }

	public DateOnly Date { get; set; }

	public string Purpose { get; set; } = null!;

	public decimal Amount { get; set; }

	public Guid StudentId { get; set; }
}
