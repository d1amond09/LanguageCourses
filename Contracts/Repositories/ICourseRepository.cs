using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;

namespace Contracts.Repositories;

public interface ICourseRepository
{
    Task<PagedList<Course>> GetAllCoursesAsync(CourseParameters courseParameters, bool trackChanges);
    Task<IEnumerable<Course>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<Course?> GetCourseAsync(Guid courseId, bool trackChanges);
    public void CreateCourse(Course course);
    public void DeleteCourse(Course course);
    IQueryable<Course> FindByCondition(Expression<Func<Course, bool>> expression, bool trackChanges = false);

}
