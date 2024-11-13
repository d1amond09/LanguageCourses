using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

internal class EmployeeRepository(LanguageCoursesContext appDbContext) :
	RepositoryBase<Employee>(appDbContext), IEmployeeRepository
{
	public void CreateEmployee(Employee Employee) => Create(Employee);

	public void DeleteEmployee(Employee Employee) => Delete(Employee);

	public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges = false) =>
		await FindAll(trackChanges)
			.Include(e => e.Courses)
			.Include(e => e.JobTitle)
			.OrderBy(c => c.Name)
			.ToListAsync();
	public async Task<Employee?> GetEmployeeAsync(Guid employeeId, bool trackChanges = false) =>
		await FindByCondition(c => c.EmployeeId.Equals(employeeId), trackChanges)
            .Include(e => e.Courses)
            .Include(e => e.JobTitle)
			.Where(x => x.Courses.Count > 0)
            .SingleOrDefaultAsync();

	public async Task<IEnumerable<Employee>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
		await FindByCondition(x => ids.Contains(x.EmployeeId), trackChanges)
            .Include(e => e.Courses)
            .Include(e => e.JobTitle)
            .ToListAsync();

	public IEnumerable<Employee> GetEmployeesTop(int rows) =>
		 [.. FindAll().Include(x => x.JobTitle).Include(x => x.Courses).Where(x => x.Courses.Count > 0).Where(x => x.JobTitle.Name.Contains("преп")).Take(rows)];
}
