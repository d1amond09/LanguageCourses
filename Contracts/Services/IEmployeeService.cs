using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;

namespace Contracts.Services;

public interface IEmployeeService
{
	Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges);
	Task<Employee?> GetEmployeeAsync(Guid id, bool trackChanges);
}
