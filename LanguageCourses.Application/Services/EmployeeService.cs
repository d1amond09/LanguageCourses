using AutoMapper;
using Contracts;
using Contracts.Services;
using LanguageCourses.Domain.DataTransferObjects;

namespace LanguageCourses.Application.Services;

internal sealed class EmployeeService(IRepositoryManager rep, ILoggerManager logger, IMapper mapper) : IEmployeeService
{
    private readonly IRepositoryManager _rep = rep;
    private readonly ILoggerManager _logger = logger;
    private readonly IMapper _mapper = mapper;
    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(bool trackChanges)
    {

        var employees = await _rep.Employees.GetAllEmployeesAsync(trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return employeesDto;

    }

    public async Task<EmployeeDto?> GetEmployeeAsync(Guid id, bool trackChanges)
    {

        var employee = await _rep.Employees.GetEmployeeAsync(id, trackChanges);
        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return employeeDto;

    }
}
