using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourses.Domain;

namespace Contracts.Repositories;

public interface IJobTitleRepository
{
	Task<IEnumerable<JobTitle>> GetAllJobTitlesAsync(bool trackChanges);
	Task<IEnumerable<JobTitle>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
	Task<JobTitle?> GetJobTitleAsync(Guid jobTitleId, bool trackChanges);
	public void CreateJobTitle(JobTitle jobTitle);
	public void DeleteJobTitle(JobTitle jobTitle);
}
