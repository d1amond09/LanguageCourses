using AutoMapper;
using Contracts;
using Contracts.Services;
using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Application.Services;

internal sealed class EmployeeService(IRepositoryManager rep, ILoggerManager logger, IMapper mapper) : IEmployeeService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;
	public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges) =>
		await _rep.Employees.GetAllEmployeesAsync(trackChanges);

	public async Task<Employee?> GetEmployeeAsync(Guid id, bool trackChanges) =>
		await _rep.Employees.GetEmployeeAsync(id, trackChanges);
}
