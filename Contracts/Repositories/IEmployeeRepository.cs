using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;

namespace Contracts.Repositories;

public interface IEmployeeRepository
{
    Task<PagedList<Employee>> GetAllEmployeesAsync(EmployeeParameters employeeParameters, bool trackChanges);
    Task<IEnumerable<Employee>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<Employee?> GetEmployeeAsync(Guid employeeId, bool trackChanges);
    public void CreateEmployee(Employee employee);
    public void DeleteEmployee(Employee employee);

    IQueryable<Employee> FindByCondition(Expression<Func<Employee, bool>> expression, bool trackChanges = false);

}
