using System.Linq.Expressions;
using Contracts;
using Contracts.Repositories;
using Contracts.Services;
using LanguageCourses.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace LanguageCourses.Application.Services;

internal sealed class PaymentService(IRepositoryManager rep, ILoggerManager logger) : IPaymentService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly ILoggerManager _logger = logger;

	public async Task<IEnumerable<Payment>> GetAllPaymentsAsync(bool trackChanges) =>
		await _rep.Payments.GetAllPaymentsAsync(trackChanges);

	public async Task<Payment?> GetPaymentAsync(Guid id, bool trackChanges) =>
		await _rep.Payments.GetPaymentAsync(id, trackChanges);
}
