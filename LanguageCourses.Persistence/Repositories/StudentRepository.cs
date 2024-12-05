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
        var students = FindAll(trackChanges).Include(c => c.Courses)
                .FilterAgeStudents(studentParameters.MinAge, studentParameters.MaxAge)
                .FilterBirthDateStudents(studentParameters.MinBirthDate, studentParameters.MaxBirthDate)
                .Search(studentParameters.SearchTerm)
                .SearchByCourse(studentParameters.Course)
                .Sort(studentParameters.OrderBy);

        var count = await students.CountAsync();

        var studentsToReturn = await students
                .Skip((studentParameters.PageNumber - 1) * studentParameters.PageSize)
                .Take(studentParameters.PageSize)
                .ToListAsync();


        return new PagedList<Student>(
            studentsToReturn,
            count,
            studentParameters.PageNumber,
            studentParameters.PageSize
        );
    }

    public async Task<Student?> GetStudentAsync(Guid studentId, bool trackChanges = false) =>
        await FindByCondition(c => c.Id.Equals(studentId), trackChanges).Include(c => c.Courses)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();



    public async Task UpdateStudentCoursesAsync(Student student, List<Guid> newCourseIds)
    {
        var existingCourseIds = student.Courses.Select(c => c.Id).ToList();
        var coursesToRemove = existingCourseIds.Except(newCourseIds).ToList();



        foreach (var courseId in coursesToRemove)
        {
            var courseToRemove = _appDbContext.Courses.Include(x => x.Students).AsNoTracking().FirstOrDefault(c => c.Id == courseId);
            if (courseToRemove != null)
            {
                courseToRemove.Students.Remove(student);
                student.Courses.Remove(courseToRemove);
            }
        }

        foreach (var courseId in newCourseIds)
        {
            if (!existingCourseIds.Contains(courseId))
            {
                var course = await _appDbContext.Courses.FindAsync(courseId);
                if (course != null)
                {
                    student.Courses.Add(course);
                }
            }
        }
        
    }
}
