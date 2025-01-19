using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;

namespace Contracts.Repositories;

public interface IJobTitleRepository
{
    Task<PagedList<JobTitle>> GetAllJobTitlesAsync(JobTitleParameters jobTitleParameters, bool trackChanges);
    Task<IEnumerable<JobTitle>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<JobTitle?> GetJobTitleAsync(Guid jobTitleId, bool trackChanges);
    JobTitle? GetJobTitleByName(string name);
    public void CreateJobTitle(JobTitle jobTitle);
    public void DeleteJobTitle(JobTitle jobTitle);
    IQueryable<JobTitle> FindByCondition(Expression<Func<JobTitle, bool>> expression, bool trackChanges = false);

}
