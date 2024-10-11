namespace Contracts.Services;

public interface IServiceManager
{
    public ICourseService CourseService { get; }
    public IEmployeeService EmployeeService { get; }
    public IJobTitleService JobTitleService { get; }
    public IPaymentService PaymentService { get; }
    public IStudentService StudentService { get; }
}
