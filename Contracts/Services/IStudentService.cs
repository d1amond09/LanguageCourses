using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;

namespace Contracts.Services;

public interface IStudentService
{
	Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges);

}
