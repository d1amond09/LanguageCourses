using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

public class JobTitleRepository(LanguageCoursesContext appDbContext) :
	RepositoryBase<JobTitle>(appDbContext), IJobTitleRepository
{
	public void CreateJobTitle(JobTitle JobTitle) => Create(JobTitle);

	public void DeleteJobTitle(JobTitle JobTitle) => Delete(JobTitle);

	public IEnumerable<Employee> GetJobtitleEmployeeAsync(bool trackChanges = false)
	{
		return FindAll(trackChanges).Select(x => x.Employees).First();
	}

	public JobTitle? GetJobTitleByName(string name) =>
		FindAll().FirstOrDefault(x => x.Name == name);

	public async Task<IEnumerable<JobTitle>> GetJobTitlesWithSalaryMoreThanAsync(decimal salary, bool trackChanges = false) =>
		await FindAll(trackChanges)
			.Where(c => c.Salary > salary)
			.OrderBy(c => c.Name)
			.ToListAsync();

	public async Task<IEnumerable<JobTitle>> GetAllJobTitlesAsync(bool trackChanges = false) =>
		await FindAll(trackChanges)
			.OrderBy(c => c.Name)
			.ToListAsync();
	public async Task<JobTitle?> GetJobTitleAsync(Guid jobTitleId, bool trackChanges = false) =>
		await FindByCondition(c => c.JobTitleId.Equals(jobTitleId), trackChanges)
			.SingleOrDefaultAsync();

	public async Task<IEnumerable<JobTitle>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
		await FindByCondition(x => ids.Contains(x.JobTitleId), trackChanges)
			.ToListAsync();
}
