using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;
using LanguageCourses.Domain.RequestFeatures;

namespace Contracts.Repositories;

public interface IStudentRepository
{
    Task<PagedList<Student>> GetAllStudentsAsync(StudentParameters studentParameters, bool trackChanges = false);
    IQueryable<Student> FindByCondition(Expression<Func<Student, bool>> expression, bool trackChanges = false);
    Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<Student?> GetStudentAsync(Guid studentId, bool trackChanges);
    void CreateStudent(Student student);
    void DeleteStudent(Student student);
    void UpdateStudent(Student student);
}
