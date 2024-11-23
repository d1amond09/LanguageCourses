using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;
using LanguageCourses.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

internal class PaymentRepository(LanguageCoursesContext appDbContext) :
    RepositoryBase<Payment>(appDbContext), IPaymentRepository
{
    public void CreatePayment(Payment Payment) => Create(Payment);

    public void DeletePayment(Payment Payment) => Delete(Payment);

    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync(bool trackChanges = false) =>
        await FindAll(trackChanges)
            .OrderBy(c => c.Purpose)
            .ToListAsync();

    public async Task<PagedList<Payment>> GetAllPaymentsAsync(PaymentParameters paymentParameters, bool trackChanges = false)
    {
        var payments =
            await FindAll(trackChanges)
                .FilterAmountPayments(paymentParameters.MinAmount, paymentParameters.MaxAmount)
                .FilterDatePayments(paymentParameters.MinDate, paymentParameters.MaxDate)
                .Search(paymentParameters.SearchTerm)
                .Sort(paymentParameters.OrderBy)
                .Skip((paymentParameters.PageNumber - 1) * paymentParameters.PageSize)
                .Take(paymentParameters.PageSize)
                .ToListAsync();

        var count = await FindAll(trackChanges).CountAsync();

        return new PagedList<Payment>(
            payments,
            count,
            paymentParameters.PageNumber,
            paymentParameters.PageSize
        );
    }

    public async Task<Payment?> GetPaymentAsync(Guid paymentId, bool trackChanges = false) =>
        await FindByCondition(c => c.Id.Equals(paymentId), trackChanges)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<Payment>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();
}
