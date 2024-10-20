using LanguageCourses.Domain.Entities;
using System.Linq.Expressions;

namespace Contracts.Services;

public interface IPaymentService
{
	public IEnumerable<Payment> GetPayments();

	public void AddPayments(string cacheKey);

	public IEnumerable<Payment>? GetPayments(string cacheKey);

	public void AddPaymentsByCondition(string cacheKey, Expression<Func<Payment, bool>> expression);
}
