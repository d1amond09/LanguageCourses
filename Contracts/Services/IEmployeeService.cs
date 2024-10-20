using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;

namespace Contracts.Services;

public interface IEmployeeService
{
	public IEnumerable<Employee> GetEmployees();

	public void AddEmployees(string cacheKey);

	public IEnumerable<Employee>? GetEmployees(string cacheKey);

	public void AddEmployeesByCondition(string cacheKey, Expression<Func<Employee, bool>> expression);
}
