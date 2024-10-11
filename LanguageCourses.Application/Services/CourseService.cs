using Contracts.Repositories;
using Contracts.Services;

namespace LanguageCourses.Application.Services;

internal sealed class CourseService(IRepositoryManager rep) : ICourseService
{
	private readonly IRepositoryManager _rep = rep;
}
