using Contracts.Repositories;

namespace Contracts;

public interface IRepositoryManager
{
    ICourseRepository Courses { get; }
    IEmployeeRepository Employees { get; }
    IJobTitleRepository JobTitles { get; }
    IPaymentRepository Payments { get; }
    IStudentRepository Students { get; }
    void SetModified<T>(T entity) where T : class;
    Task SaveAsync();
    void SaveChanges();
}
