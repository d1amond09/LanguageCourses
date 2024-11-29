using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;
using LanguageCourses.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

internal class StudentRepository(LanguageCoursesContext appDbContext) :
    RepositoryBase<Student>(appDbContext), IStudentRepository
{
    public void CreateStudent(Student student) => Create(student);
    public void UpdateStudent(Student student) => Update(student);

    public void DeleteStudent(Student student) => Delete(student);

    public async Task<PagedList<Student>> GetAllStudentsAsync(StudentParameters studentParameters, bool trackChanges = false)
    {
        var students =
            await FindAll(trackChanges).Include(c => c.Courses)
                .FilterAgeStudents(studentParameters.MinAge, studentParameters.MaxAge)
                .FilterBirthDateStudents(studentParameters.MinBirthDate, studentParameters.MaxBirthDate)
                .Search(studentParameters.SearchTerm)
                .Sort(studentParameters.OrderBy)
                .Skip((studentParameters.PageNumber - 1) * studentParameters.PageSize)
                .Take(studentParameters.PageSize)
                .ToListAsync();

        var count = await FindAll(trackChanges).CountAsync();

        return new PagedList<Student>(
            students,
            count,
            studentParameters.PageNumber,
            studentParameters.PageSize
        );
    }

    public async Task<Student?> GetStudentAsync(Guid studentId, bool trackChanges = false) =>
        await FindByCondition(c => c.Id.Equals(studentId), trackChanges)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();
}
