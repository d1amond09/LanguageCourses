using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;

namespace Contracts.Services;

public interface IStudentService
{
	public IEnumerable<Student> GetStudents();

	public void AddStudents(string cacheKey);

	public IEnumerable<Student>? GetStudents(string cacheKey);

	public void AddStudentsByCondition(string cacheKey, Expression<Func<Student, bool>> expression);

}
