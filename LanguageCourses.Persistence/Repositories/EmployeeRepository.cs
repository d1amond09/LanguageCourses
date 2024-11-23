using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

internal class EmployeeRepository(LanguageCoursesContext appDbContext) :
    RepositoryBase<Employee>(appDbContext), IEmployeeRepository
{
    public void CreateEmployee(Employee Employee) => Create(Employee);

    public void DeleteEmployee(Employee Employee) => Delete(Employee);

    public async Task<PagedList<Employee>> GetAllEmployeesAsync(EmployeeParameters employeeParameters, bool trackChanges = false)
    {
        var employees =
            await FindAll(trackChanges).Include(e => e.Courses)
                .FilterByEducation(employeeParameters.Education)
                .FilterByJobTitle(employeeParameters.JobTitle)
                .Search(employeeParameters.SearchTerm)
                .Sort(employeeParameters.OrderBy)
                .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
                .Take(employeeParameters.PageSize)
                .ToListAsync();

        var count = await FindAll(trackChanges).CountAsync();

        return new PagedList<Employee>(
            employees,
            count,
            employeeParameters.PageNumber,
            employeeParameters.PageSize
        );
    }

    public async Task<Employee?> GetEmployeeAsync(Guid employeeId, bool trackChanges = false) =>
        await FindByCondition(c => c.Id.Equals(employeeId), trackChanges)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<Employee>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();
}
