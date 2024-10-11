namespace Contracts.Repositories;

public interface IRepositoryManager
{
	ICourseRepository Courses { get; }
	IEmployeeRepository Employees { get; }
	IJobTitleRepository JobTitles { get; }
	IPaymentRepository Payments { get; }
	IStudentRepository Students { get; }
	Task SaveAsync();
	void SaveChanges();
}
