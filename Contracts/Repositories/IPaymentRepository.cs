using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourses.Domain;

namespace Contracts.Repositories;

public interface IPaymentRepository
{
	Task<IEnumerable<Payment>> GetAllPaymentsAsync(bool trackChanges);
	Task<IEnumerable<Payment>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
	Task<Payment?> GetPaymentAsync(Guid paymentId, bool trackChanges);
	public void CreatePayment(Payment payment);
	public void DeletePayment(Payment payment);
}
