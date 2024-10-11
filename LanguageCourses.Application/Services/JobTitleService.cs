using Contracts.Repositories;
using Contracts.Services;

namespace LanguageCourses.Application.Services;

internal sealed class JobTitleService(IRepositoryManager rep) : IJobTitleService
{
	private readonly IRepositoryManager _rep = rep;
}
