using LanguageCourses.Domain.DataTransferObjects;

namespace Contracts.Services;

public interface IJobTitleService
{
	Task<IEnumerable<JobTitleDto>> GetAllJobTitlesAsync(bool trackChanges);
	Task<JobTitleDto?> GetJobTitleAsync(Guid id, bool trackChanges);
}
