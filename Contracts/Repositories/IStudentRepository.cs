using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;

namespace Contracts.Repositories;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges);
    IQueryable<Student> FindByCondition(Expression<Func<Student, bool>> expression, bool trackChanges = false);
    Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<Student?> GetStudentAsync(Guid studentId, bool trackChanges);
    void CreateStudent(Student student);
    void DeleteStudent(Student student);
    void UpdateStudent(Student student);
}
