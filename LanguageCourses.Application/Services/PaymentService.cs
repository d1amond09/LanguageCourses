using Contracts.Repositories;
using Contracts.Services;

namespace LanguageCourses.Application.Services;

internal sealed class PaymentService(IRepositoryManager rep) : IPaymentService
{
	private readonly IRepositoryManager _rep = rep;
}
