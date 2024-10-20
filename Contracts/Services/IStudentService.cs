using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Entities.DataTransferObjects;

namespace Contracts.Services;

public interface IStudentService
{
	Task<IEnumerable<StudentDto>> GetAllStudentsAsync(bool trackChanges);
	Task<Student?> GetStudentAsync(Guid id, bool trackChanges);

}
