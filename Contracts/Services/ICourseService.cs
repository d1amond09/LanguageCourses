using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;

namespace Contracts.Services;

public interface ICourseService
{
	Task<IEnumerable<Course>> GetAllCoursesAsync(bool trackChanges);
	Task<Course?> GetCourseAsync(Guid id, bool trackChanges);
}
