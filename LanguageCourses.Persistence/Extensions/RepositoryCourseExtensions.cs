using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Persistence.Extensions.Utility;

namespace LanguageCourses.Persistence.Extensions;

public static class RepositoryCourseExtensions
{
    public static IQueryable<Course> FilterTuitionFeeCourses(this IQueryable<Course> courses, double minTuitionFee, double maxTuitionFee) =>
        courses.Where(e => e.TuitionFee >= minTuitionFee && e.TuitionFee <= maxTuitionFee);

    public static IQueryable<Course> FilterHoursCourses(this IQueryable<Course> courses, int minHours, int maxHours) =>
        courses.Where(e => e.Hours >= minHours && e.Hours <= maxHours);

    public static IQueryable<Course> SearchByTrainingProgram(this IQueryable<Course> courses, string searchTrainingProgram)
    {
        if (string.IsNullOrWhiteSpace(searchTrainingProgram))
            return courses;

        var lowerCaseTerm = searchTrainingProgram.Trim().ToLower();
        return courses.Where(e => e.TrainingProgram!.ToLower().Contains(searchTrainingProgram));
    }

    public static IQueryable<Course> Search(this IQueryable<Course> courses, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return courses;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return courses.Where(e => e.Name!.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Course> Sort(this IQueryable<Course> courses, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return courses.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Course>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return courses.OrderBy(e => e.Name);

        return courses.OrderBy(orderQuery);
    }
}
