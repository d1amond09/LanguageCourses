using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

internal class StudentRepository(LanguageCoursesContext appDbContext) :
	RepositoryBase<Student>(appDbContext), IStudentRepository
{
	public void CreateStudent(Student student) => Create(student);
	public void UpdateStudent(Student student) => Update(student);

	public void DeleteStudent(Student student) => Delete(student);

	public async Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges = false) =>
		await FindAll(trackChanges)
			.Include(s => s.Courses)
			.Include(s => s.Payments)
			.Take(300)
			.OrderBy(c => c.Name)
			.ToListAsync();

	public async Task<Student?> GetStudentAsync(Guid studentId, bool trackChanges = false) =>
		await FindByCondition(c => c.StudentId.Equals(studentId), trackChanges)
            .Include(s => s.Courses)
            .Include(s => s.Payments)
            .SingleOrDefaultAsync();

	public async Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
		await FindByCondition(x => ids.Contains(x.StudentId), trackChanges)
            .Include(s => s.Courses)
            .Include(s => s.Payments)
            .ToListAsync();

	public IEnumerable<Student> GetStudentsTop(int rows) =>
		 [.. FindAll()
			.Include(s => s.Payments)
			.Include(c => c.Courses)
			.Take(rows)];
}
