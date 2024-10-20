﻿using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;

namespace Contracts.Repositories;

public interface IPaymentRepository
{
	Task<IEnumerable<Payment>> GetAllPaymentsAsync(bool trackChanges);
	Task<IEnumerable<Payment>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
	Task<Payment?> GetPaymentAsync(Guid paymentId, bool trackChanges);
	public void CreatePayment(Payment payment);
	public void DeletePayment(Payment payment);
	IQueryable<Payment> FindByCondition(Expression<Func<Payment, bool>> expression, bool trackChanges = false);

}
