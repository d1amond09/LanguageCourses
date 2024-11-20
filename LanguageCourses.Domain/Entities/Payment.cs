namespace LanguageCourses.Domain.Entities;

public class Payment
{
    public Guid PaymentId { get; set; }

    public DateOnly Date { get; set; }

    public string Purpose { get; set; } = null!;

    public decimal Amount { get; set; }

    public Guid StudentId { get; set; }

    public virtual Student Student { get; set; } = null!;
}
