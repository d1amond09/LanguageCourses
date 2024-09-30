using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourses.Domain;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

public class EmployeeRepository(LanguageCoursesContext appDbContext) :
	RepositoryBase<Employee>(appDbContext), IEmployeeRepository
{
	public void CreateEmployee(Employee Employee) => Create(Employee);

	public void DeleteEmployee(Employee Employee) => Delete(Employee);

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
