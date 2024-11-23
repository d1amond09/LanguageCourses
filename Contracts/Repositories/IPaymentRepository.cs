using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;

namespace Contracts.Repositories;

public interface IPaymentRepository
{
    Task<PagedList<Payment>> GetAllPaymentsAsync(PaymentParameters paymentParameters, bool trackChanges);
    Task<IEnumerable<Payment>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<Payment?> GetPaymentAsync(Guid paymentId, bool trackChanges);
    public void CreatePayment(Payment payment);
    public void DeletePayment(Payment payment);
    IQueryable<Payment> FindByCondition(Expression<Func<Payment, bool>> expression, bool trackChanges = false);

}
