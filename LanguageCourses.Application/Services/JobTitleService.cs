using Contracts;
using Contracts.Services;
using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Application.Services;

internal sealed class JobTitleService(IRepositoryManager rep, ILoggerManager logger) : IJobTitleService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly ILoggerManager _logger = logger;

	public async Task<IEnumerable<JobTitle>> GetAllJobTitlesAsync(bool trackChanges) =>
		await _rep.JobTitles.GetAllJobTitlesAsync(trackChanges);

	public async Task<JobTitle?> GetJobTitleAsync(Guid id, bool trackChanges) =>
		await _rep.JobTitles.GetJobTitleAsync(id, trackChanges);
}