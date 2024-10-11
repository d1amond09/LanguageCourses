using Contracts.Repositories;
using Contracts.Services;

namespace LanguageCourses.Application.Services;

internal sealed class EmployeeService(IRepositoryManager rep) : IEmployeeService
{
	private readonly IRepositoryManager _rep = rep;
}
