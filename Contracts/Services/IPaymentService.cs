using LanguageCourses.Domain.DataTransferObjects;

namespace Contracts.Services;

public interface IPaymentService
{
    Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync(bool trackChanges);

    Task<PaymentDto?> GetPaymentAsync(Guid id, bool trackChanges);
}
