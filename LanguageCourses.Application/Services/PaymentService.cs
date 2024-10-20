using AutoMapper;
using Contracts;
using Contracts.Services;
using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Application.Services;

internal sealed class PaymentService(IRepositoryManager rep, ILoggerManager logger, IMapper mapper) : IPaymentService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;
	public async Task<IEnumerable<Payment>> GetAllPaymentsAsync(bool trackChanges) =>
		await _rep.Payments.GetAllPaymentsAsync(trackChanges);

	public async Task<Payment?> GetPaymentAsync(Guid id, bool trackChanges) =>
		await _rep.Payments.GetPaymentAsync(id, trackChanges);
}
