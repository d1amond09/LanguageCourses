using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

public class StudentRepository(LanguageCoursesContext appDbContext) :
	RepositoryBase<Student>(appDbContext), IStudentRepository
{
	public void CreateStudent(Student student) => Create(student);
	public void UpdateStudent(Student student) => Update(student);

	public void DeleteStudent(Student student) => Delete(student);

	public async Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges = false) =>
		await FindAll(trackChanges)
			.OrderBy(c => c.Name)
			.ToListAsync();

	public Student? GetStudentByPassport(string passport, bool trackChanges = false) =>
		FindByCondition(c => c.PassportNumber.Equals(passport), trackChanges)
			.SingleOrDefault();

	public async Task<Student?> GetStudentByPassportAsync(string passport, bool trackChanges = false) =>
		await FindByCondition(c => c.PassportNumber.Equals(passport), trackChanges)
			.SingleOrDefaultAsync();

	public async Task<Student?> GetStudentAsync(Guid studentId, bool trackChanges = false) =>
		await FindByCondition(c => c.StudentId.Equals(studentId), trackChanges)
			.SingleOrDefaultAsync();

	public async Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
		await FindByCondition(x => ids.Contains(x.StudentId), trackChanges)
			.ToListAsync();

	public IEnumerable<Student> GetStudentsTop(int rows) =>
		 [.. FindAll().Take(rows)
			.Include(s => s.Payments)
			.Include(c => c.Courses)
			.Where(c => c.Courses.Count > 0)];
}
