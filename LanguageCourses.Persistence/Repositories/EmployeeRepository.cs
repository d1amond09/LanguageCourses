using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

public class EmployeeRepository(LanguageCoursesContext appDbContext) :
	RepositoryBase<Employee>(appDbContext), IEmployeeRepository
{
	public void CreateEmployee(Employee Employee) => Create(Employee);

	public void DeleteEmployee(Employee Employee) => Delete(Employee);

	public async Task<IEnumerable<(JobTitle jobTitle, Employee employee)>> GetEmployeesJobtitlesAsync(bool trackChanges = false)
	{
		var query = FindAll(trackChanges);

		if (!trackChanges)
		{
			query = query.AsNoTracking();
		}

		var employees = await query.ToListAsync();
		var jobTitles = await new JobTitleRepository(AppDbContext).GetAllJobTitlesAsync();

		var result = employees
			.Join(
				jobTitles,
				employee => employee.JobTitleId,
				jobTitle => jobTitle.JobTitleId,
				(employee, jobTitle) => new { employee, jobTitle }
			)
			.ToList();

		return result.Select(r => (r.jobTitle, r.employee));
	}

	public async Task<IEnumerable<(JobTitle jobTitle, Employee employee)>> GetEmployeesJobtitlesWithFilterSalaryAsync(decimal salary, bool trackChanges = false)
	{
		var query = FindAll(trackChanges);

		if (!trackChanges)
		{
			query = query.AsNoTracking();
		}

		var employees = await query.ToListAsync();
		var jobTitles = await new JobTitleRepository(AppDbContext)
			.GetAllJobTitlesAsync();

		var result = employees
			.Join(
				jobTitles,
				employee => employee.JobTitleId,
				jobTitle => jobTitle.JobTitleId,
				(employee, jobTitle) => new { employee, jobTitle }
			)
			.ToList()
			.Where(x => x.jobTitle.Salary > salary);

		return result.Select(r => (r.jobTitle, r.employee));
	}

	public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges = false) =>
		await FindAll(trackChanges)
			.OrderBy(c => c.Name)
			.ToListAsync();
	public async Task<Employee?> GetEmployeeAsync(Guid employeeId, bool trackChanges = false) =>
		await FindByCondition(c => c.EmployeeId.Equals(employeeId), trackChanges)
			.SingleOrDefaultAsync();

	public async Task<IEnumerable<Employee>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
		await FindByCondition(x => ids.Contains(x.EmployeeId), trackChanges)
			.ToListAsync();
}
