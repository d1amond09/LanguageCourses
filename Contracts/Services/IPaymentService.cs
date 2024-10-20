using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;

namespace Contracts.Services;

public interface IPaymentService
{
	public async Task<IEnumerable<Payment>> GetAllPaymentsAsync(bool trackChanges);
	public async Task<Payment?> GetPaymentAsync(Guid id, bool trackChanges);
}
