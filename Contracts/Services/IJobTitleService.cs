using LanguageCourses.Domain.Entities;

namespace Contracts.Services;

public interface IJobTitleService
{
	Task<IEnumerable<JobTitle>> GetAllJobTitlesAsync(bool trackChanges);
	Task<JobTitle?> GetJobTitleAsync(Guid id, bool trackChanges);
}
