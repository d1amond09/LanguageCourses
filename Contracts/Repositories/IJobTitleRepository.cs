using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;

namespace Contracts.Repositories;

public interface IJobTitleRepository
{
	Task<IEnumerable<JobTitle>> GetAllJobTitlesAsync(bool trackChanges);
	Task<IEnumerable<JobTitle>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
	Task<JobTitle?> GetJobTitleAsync(Guid jobTitleId, bool trackChanges);
	JobTitle? GetJobTitleByName(string name);
	public void CreateJobTitle(JobTitle jobTitle);
	public void DeleteJobTitle(JobTitle jobTitle);
	IQueryable<JobTitle> FindByCondition(Expression<Func<JobTitle, bool>> expression, bool trackChanges = false);

}
