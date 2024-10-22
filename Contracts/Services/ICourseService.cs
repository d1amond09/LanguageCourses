using LanguageCourses.Domain.DataTransferObjects;

namespace Contracts.Services;

public interface ICourseService
{
	Task<IEnumerable<CourseDto>> GetAllCoursesAsync(bool trackChanges);
	Task<CourseDto?> GetCourseAsync(Guid id, bool trackChanges);
}
