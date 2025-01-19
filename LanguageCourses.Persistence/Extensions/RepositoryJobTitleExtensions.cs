using System.Linq.Dynamic.Core;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Persistence.Extensions.Utility;

namespace LanguageCourses.Persistence.Extensions;

public static class RepositoryJobTitleExtensions
{
    public static IQueryable<JobTitle> FilterBySalary(this IQueryable<JobTitle> jobTitles, double minSalary, double maxSalary) =>
        jobTitles.Where(j => j.Salary > minSalary && j.Salary < maxSalary);

    public static IQueryable<JobTitle> Search(this IQueryable<JobTitle> jobTitles, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return jobTitles;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return jobTitles.Where(e => e.Name!.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<JobTitle> Sort(this IQueryable<JobTitle> jobTitles, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return jobTitles.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<JobTitle>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return jobTitles.OrderBy(e => e.Name);

        return jobTitles.OrderBy(orderQuery);
    }
}
