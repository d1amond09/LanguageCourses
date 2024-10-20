using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;

namespace Contracts.Services;

public interface IStudentService
{
	Task<IEnumerable<StudentDto>> GetAllStudentsAsync(bool trackChanges);
	Task<Student?> GetStudentAsync(Guid id, bool trackChanges);

}
