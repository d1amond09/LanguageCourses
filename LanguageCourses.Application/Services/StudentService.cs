using Contracts.Repositories;
using Contracts.Services;

namespace LanguageCourses.Application.Services;

internal sealed class StudentService(IRepositoryManager rep) : IStudentService
{
	private readonly IRepositoryManager _rep = rep;
}
