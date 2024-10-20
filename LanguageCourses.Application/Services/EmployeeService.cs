using System.Linq.Expressions;
using Contracts.Repositories;
using Contracts.Services;
using LanguageCourses.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace LanguageCourses.Application.Services;

internal sealed class EmployeeService(IRepositoryManager rep, IMemoryCache memoryCache) : IEmployeeService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly IMemoryCache _cache = memoryCache;
	private int _rowsNumber = 20;

	public IEnumerable<Employee> GetEmployees()
	{
		return _rep.Employees.GetEmployeesTop(_rowsNumber);
	}

	public void AddEmployees(string cacheKey)
	{
		IEnumerable<Employee> employees = _rep.Employees.GetEmployeesTop(_rowsNumber);

		_cache.Set(cacheKey, employees, new MemoryCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(248)
		});
	}

	public void AddEmployeesByCondition(string cacheKey, Expression<Func<Employee, bool>> expression)
	{
		IEnumerable<Employee> employees = _rep.Employees.FindByCondition(expression).Take(_rowsNumber);

		_cache.Set(cacheKey, employees, new MemoryCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(248)
		});
	}

	public IEnumerable<Employee>? GetEmployees(string cacheKey)
	{
		if (!_cache.TryGetValue(cacheKey, out IEnumerable<Employee>? employees))
		{
			employees = _rep.Employees.GetEmployeesTop(_rowsNumber);
			if (employees != null)
			{
				_cache.Set(cacheKey, employees,
				new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(248)));
			}
		}
		return employees;
	}
}
