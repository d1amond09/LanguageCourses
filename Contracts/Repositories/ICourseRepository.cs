using LanguageCourses.Domain.Entities;

namespace Contracts.Repositories;

public interface ICourseRepository
{
	Task<IEnumerable<Course>> GetAllCoursesAsync(bool trackChanges);
	Task<IEnumerable<Course>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
	Task<Course?> GetCourseAsync(Guid courseId, bool trackChanges);
	public void CreateCourse(Course course);
	public void DeleteCourse(Course course);
}
