using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;
using LanguageCourses.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

internal class JobTitleRepository(LanguageCoursesContext appDbContext) :
    RepositoryBase<JobTitle>(appDbContext), IJobTitleRepository
{
    public void CreateJobTitle(JobTitle JobTitle) => Create(JobTitle);

    public void DeleteJobTitle(JobTitle JobTitle) => Delete(JobTitle);

    public JobTitle? GetJobTitleByName(string name) =>
        FindAll().FirstOrDefault(x => x.Name == name);

    public async Task<PagedList<JobTitle>> GetAllJobTitlesAsync(JobTitleParameters jobTitleParameters, bool trackChanges = false)
    {
        var jobTitles = FindAll(trackChanges)
                .FilterBySalary(jobTitleParameters.MinSalary, jobTitleParameters.MaxSalary)
                .Search(jobTitleParameters.SearchTerm)
                .Sort(jobTitleParameters.OrderBy);

        var count = await jobTitles.CountAsync();

        var jobTitlesToReturn = await jobTitles
                .Skip((jobTitleParameters.PageNumber - 1) * jobTitleParameters.PageSize)
                .Take(jobTitleParameters.PageSize)
                .ToListAsync();

        return new PagedList<JobTitle>(
            jobTitlesToReturn,
            count,
            jobTitleParameters.PageNumber,
            jobTitleParameters.PageSize
        );
    }

    public async Task<JobTitle?> GetJobTitleAsync(Guid jobTitleId, bool trackChanges = false) =>
        await FindByCondition(c => c.Id.Equals(jobTitleId), trackChanges)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<JobTitle>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();
}
