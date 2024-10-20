using Contracts;
using Contracts.Services;
using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Application.Services;

internal sealed class CourseService(IRepositoryManager rep, ILoggerManager logger) : ICourseService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly ILoggerManager _logger = logger;

	public async Task<IEnumerable<Course>> GetAllCoursesAsync(bool trackChanges) =>
		await _rep.Courses.GetAllCoursesAsync(trackChanges);

	public async Task<Course?> GetCourseAsync(Guid id, bool trackChanges) =>
		await _rep.Courses.GetCourseAsync(id, trackChanges);
}