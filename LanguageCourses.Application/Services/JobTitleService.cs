using System.Linq.Expressions;
using Contracts.Repositories;
using Contracts.Services;
using LanguageCourses.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace LanguageCourses.Application.Services;

internal sealed class JobTitleService(IRepositoryManager rep, IMemoryCache memoryCache) : IJobTitleService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly IMemoryCache _cache = memoryCache;
	private int _rowsNumber = 20;

	public IEnumerable<JobTitle> GetJobTitles()
	{
		return _rep.JobTitles.GetJobTitlesTop(_rowsNumber);
	}

	public void AddJobTitles(string cacheKey)
	{
		IEnumerable<JobTitle> jobTitles = _rep.JobTitles.GetJobTitlesTop(_rowsNumber);

		_cache.Set(cacheKey, jobTitles, new MemoryCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(248)
		});
	}

	public void AddJobTitlesByCondition(string cacheKey, Expression<Func<JobTitle, bool>> expression)
	{
		IEnumerable<JobTitle> jobTitles = _rep.JobTitles.FindByCondition(expression).Take(_rowsNumber);

		_cache.Set(cacheKey, jobTitles, new MemoryCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(248)
		});
	}

	public IEnumerable<JobTitle>? GetJobTitles(string cacheKey)
	{
		if (!_cache.TryGetValue(cacheKey, out IEnumerable<JobTitle>? jobTitles))
		{
			jobTitles = _rep.JobTitles.GetJobTitlesTop(_rowsNumber);
			if (jobTitles != null)
			{
				_cache.Set(cacheKey, jobTitles,
				new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(248)));
			}
		}
		return jobTitles;
	}
}