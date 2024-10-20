using System.Linq.Expressions;
using Contracts.Repositories;
using Contracts.Services;
using LanguageCourses.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace LanguageCourses.Application.Services;

internal sealed class PaymentService(IRepositoryManager rep, IMemoryCache memoryCache) : IPaymentService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly IMemoryCache _cache = memoryCache;
	private int _rowsNumber = 20;

	public IEnumerable<Payment> GetPayments()
	{
		return _rep.Payments.GetPaymentsTop(_rowsNumber);
	}

	public void AddPayments(string cacheKey)
	{
		IEnumerable<Payment> payments = _rep.Payments.GetPaymentsTop(_rowsNumber);

		_cache.Set(cacheKey, payments, new MemoryCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(248)
		});
	}

	public void AddPaymentsByCondition(string cacheKey, Expression<Func<Payment, bool>> expression)
	{
		IEnumerable<Payment> payments = _rep.Payments.FindByCondition(expression).Take(_rowsNumber);

		_cache.Set(cacheKey, payments, new MemoryCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(248)
		});
	}

	public IEnumerable<Payment>? GetPayments(string cacheKey)
	{
		if (!_cache.TryGetValue(cacheKey, out IEnumerable<Payment>? payments))
		{
			payments = _rep.Payments.GetPaymentsTop(_rowsNumber);
			if (payments != null)
			{
				_cache.Set(cacheKey, payments,
				new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(248)));
			}
		}
		return payments;
	}
}
