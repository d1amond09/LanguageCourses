using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourses.Domain;

namespace Contracts.Repositories;

public interface IEmployeeRepository
{
	Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges);
	Task<IEnumerable<Employee>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
	Task<Employee?> GetEmployeeAsync(Guid employeeId, bool trackChanges);
	public void CreateEmployee(Employee employee);
	public void DeleteEmployee(Employee employee);
}
