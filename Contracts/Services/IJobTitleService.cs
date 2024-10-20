using LanguageCourses.Domain.Entities;
using System.Linq.Expressions;

namespace Contracts.Services;

public interface IJobTitleService
{
	public IEnumerable<JobTitle> GetJobTitles();

	public void AddJobTitles(string cacheKey);

	public IEnumerable<JobTitle>? GetJobTitles(string cacheKey);

	public void AddJobTitlesByCondition(string cacheKey, Expression<Func<JobTitle, bool>> expression);
}
