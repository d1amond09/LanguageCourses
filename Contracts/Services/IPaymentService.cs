using LanguageCourses.Domain.Entities;

namespace Contracts.Services;

public interface IPaymentService
{
	Task<IEnumerable<Payment>> GetAllPaymentsAsync(bool trackChanges);
	Task<Payment?> GetPaymentAsync(Guid id, bool trackChanges);
}
