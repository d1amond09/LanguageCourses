using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Domain.DataTransferObjects;

public record PaymentDto
{
	public Guid PaymentId { get; set; }

	public DateOnly Date { get; set; }

	public string? Purpose { get; set; } 

	public decimal Amount { get; set; }

	public Guid StudentId { get; set; }
    public StudentDto? Student { get; set; }
}
