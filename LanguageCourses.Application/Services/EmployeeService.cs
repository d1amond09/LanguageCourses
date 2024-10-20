using System.Linq.Expressions;
using Contracts;
using Contracts.Repositories;
using Contracts.Services;
using LanguageCourses.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace LanguageCourses.Application.Services;

internal sealed class EmployeeService(IRepositoryManager rep, ILoggerManager logger) : IEmployeeService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly ILoggerManager _logger = logger;

	public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges) =>
		await _rep.Employees.GetAllEmployeesAsync(trackChanges);

	public async Task<Employee?> GetEmployeeAsync(Guid id, bool trackChanges) =>
		await _rep.Employees.GetEmployeeAsync(id, trackChanges);
}
