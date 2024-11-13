using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

internal class JobTitleRepository(LanguageCoursesContext appDbContext) :
	RepositoryBase<JobTitle>(appDbContext), IJobTitleRepository
{
	public void CreateJobTitle(JobTitle JobTitle) => Create(JobTitle);

	public void DeleteJobTitle(JobTitle JobTitle) => Delete(JobTitle);

	public JobTitle? GetJobTitleByName(string name) =>
		FindAll().Include(j => j.Employees).FirstOrDefault(x => x.Name == name);

	public async Task<IEnumerable<JobTitle>> GetAllJobTitlesAsync(bool trackChanges = false) =>
		await FindAll(trackChanges)
			.Include(j => j.Employees)
			.OrderBy(c => c.Salary)
			.ToListAsync();
	public async Task<JobTitle?> GetJobTitleAsync(Guid jobTitleId, bool trackChanges = false) =>
		await FindByCondition(c => c.JobTitleId.Equals(jobTitleId), trackChanges)
            .Include(j => j.Employees)
            .SingleOrDefaultAsync();

	public async Task<IEnumerable<JobTitle>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
		await FindByCondition(x => ids.Contains(x.JobTitleId), trackChanges)
			.Include(j => j.Employees)
			.ToListAsync();

	public IEnumerable<JobTitle> GetJobTitlesTop(int rows) =>
		 [.. FindAll().Include(c => c.Employees).Where(c => c.Employees.Count > 0).Take(rows)];
}
