using LanguageCourses.Domain.DataTransferObjects;

namespace Contracts.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(bool trackChanges);
    Task<EmployeeDto?> GetEmployeeAsync(Guid id, bool trackChanges);
}
