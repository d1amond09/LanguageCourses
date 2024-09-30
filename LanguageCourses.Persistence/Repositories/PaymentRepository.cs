using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourses.Domain;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

public class PaymentRepository(LanguageCoursesContext appDbContext) :
	RepositoryBase<Payment>(appDbContext), IPaymentRepository
{
	public void CreatePayment(Payment Payment) => Create(Payment);

	public void DeletePayment(Payment Payment) => Delete(Payment);

	public async Task<IEnumerable<Payment>> GetAllPaymentsAsync(bool trackChanges = false) =>
		await FindAll(trackChanges)
			.OrderBy(c => c.Purpose)
			.ToListAsync();
	public async Task<Payment?> GetPaymentAsync(Guid paymentId, bool trackChanges = false) =>
		await FindByCondition(c => c.PaymentId.Equals(paymentId), trackChanges)
			.SingleOrDefaultAsync();

	public async Task<IEnumerable<Payment>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
		await FindByCondition(x => ids.Contains(x.PaymentId), trackChanges)
			.ToListAsync();
}
