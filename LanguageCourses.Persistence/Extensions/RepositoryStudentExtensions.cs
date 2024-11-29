using System.Linq.Dynamic.Core;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Persistence.Extensions.Utility;

namespace LanguageCourses.Persistence.Extensions;

public static class RepositoryStudentExtensions
{
    public static IQueryable<Student> FilterAgeStudents(this IQueryable<Student> students, int minAge, int maxAge)
    {
        var today = DateTime.Now;

        return students.Where(e =>
            (today.Year - e.BirthDate.Year) -
            (today.DayOfYear < e.BirthDate.DayOfYear ? 1 : 0) >= minAge &&
            (today.Year - e.BirthDate.Year) -
            (today.DayOfYear < e.BirthDate.DayOfYear ? 1 : 0) <= maxAge);
    }

    public static IQueryable<Student> FilterBirthDateStudents(this IQueryable<Student> students, DateOnly minBirthDate, DateOnly maxBirthDate) =>
        students.Where(e => e.BirthDate >= minBirthDate && e.BirthDate <= maxBirthDate);

    public static IQueryable<Student> SearchByCourse(this IQueryable<Student> students, Guid? searchCourse)
    {
        if (searchCourse == null)
            return students;

        return students.Where(s => s.Courses.Any(c => c.Id == searchCourse));
    }

    public static IQueryable<Student> Search(this IQueryable<Student> students, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return students;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return students.Where(e => e.Surname!.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Student> Sort(this IQueryable<Student> students, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return students.OrderBy(e => e.Surname);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Student>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return students.OrderBy(e => e.Surname);

        return students.OrderBy(orderQuery);
    }
}
