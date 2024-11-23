using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Persistence.Extensions.Utility;

namespace LanguageCourses.Persistence.Extensions;

public static class RepositoryPaymentExtensions
{
    public static IQueryable<Payment> FilterAmountPayments(this IQueryable<Payment> payments, double minAmount, double maxAmount) =>
        payments.Where(e => e.Amount >= minAmount && e.Amount <= maxAmount);

    public static IQueryable<Payment> FilterDatePayments(this IQueryable<Payment> payments, DateOnly minBirthDate, DateOnly maxBirthDate) =>
        payments.Where(e => e.Date >= minBirthDate && e.Date <= maxBirthDate);

    public static IQueryable<Payment> Search(this IQueryable<Payment> payments, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return payments;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return payments.Where(e => e.Purpose!.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Payment> Sort(this IQueryable<Payment> payments, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return payments.OrderBy(e => e.Date);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Payment>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return payments.OrderBy(e => e.Date);

        return payments.OrderBy(orderQuery);
    }
}
