using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourses.Domain;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

public class JobTitleRepository(LanguageCoursesContext appDbContext) :
	RepositoryBase<JobTitle>(appDbContext), IJobTitleRepository
{
	public void CreateJobTitle(JobTitle JobTitle) => Create(JobTitle);

	public void DeleteJobTitle(JobTitle JobTitle) => Delete(JobTitle);

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
