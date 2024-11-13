using AutoMapper;
using Contracts;
using Contracts.Services;
using LanguageCourses.Domain.DataTransferObjects;

namespace LanguageCourses.Application.Services;

internal sealed class JobTitleService(IRepositoryManager rep, ILoggerManager logger, IMapper mapper) : IJobTitleService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;
	public async Task<IEnumerable<JobTitleDto>> GetAllJobTitlesAsync(bool trackChanges)
	{

		var jobtitles = await _rep.JobTitles.GetAllJobTitlesAsync(trackChanges);
		var jobtitlesDto = _mapper.Map<IEnumerable<JobTitleDto>>(jobtitles);
		return jobtitlesDto;

	}

	public async Task<JobTitleDto?> GetJobTitleAsync(Guid id, bool trackChanges)
	{
		var jobtitle = await _rep.JobTitles.GetJobTitleAsync(id, trackChanges);
		var jobtitleDto = _mapper.Map<JobTitleDto>(jobtitle);
		return jobtitleDto;
	}
}