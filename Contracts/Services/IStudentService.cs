using LanguageCourses.Domain.DataTransferObjects;

namespace Contracts.Services;

public interface IStudentService
{
	Task<IEnumerable<StudentDto>> GetAllStudentsAsync(bool trackChanges);
	Task<StudentDto?> GetStudentAsync(Guid id, bool trackChanges);

}
