using AutoMapper;
using Contracts;
using Contracts.Services;
using LanguageCourses.Domain.DataTransferObjects;

namespace LanguageCourses.Application.Services;

internal sealed class PaymentService(IRepositoryManager rep, ILoggerManager logger, IMapper mapper) : IPaymentService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;
	public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync(bool trackChanges)
	{
		var payments = await _rep.Payments.GetAllPaymentsAsync(trackChanges);
		var paymentsDto = _mapper.Map<IEnumerable<PaymentDto>>(payments);
		return paymentsDto;
	}

	public async Task<PaymentDto?> GetPaymentAsync(Guid id, bool trackChanges)
	{
		var payment = await _rep.Payments.GetPaymentAsync(id, trackChanges);
		var paymentDto = _mapper.Map<PaymentDto>(payment);
		return paymentDto;
	}
}
