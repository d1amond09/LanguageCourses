using System.Linq.Dynamic.Core;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Persistence.Extensions.Utility;
using Microsoft.IdentityModel.Tokens;

namespace LanguageCourses.Persistence.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> FilterByEducation(this IQueryable<Employee> courses, string education)
    {
        if (education.IsNullOrEmpty())
            return courses;
        else
            return courses.Where(e => e.Education == education);

    }

    public static IQueryable<Employee> FilterByJobTitle(this IQueryable<Employee> courses, Guid? jobtitle)
    {
        if (jobtitle == null)
            return courses;
        return courses.Where(e => e.JobTitleId == jobtitle);
    }

    public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return employees;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return employees.Where(e => e.Surname!.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return employees.OrderBy(e => e.Surname);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return employees.OrderBy(e => e.Surname);

        return employees.OrderBy(orderQuery);
    }
}
