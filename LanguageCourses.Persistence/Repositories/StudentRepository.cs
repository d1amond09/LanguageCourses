using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourses.Domain;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

public class StudentRepository(LanguageCoursesContext appDbContext) :
	RepositoryBase<Student>(appDbContext), IStudentRepository
{
	public void CreateStudent(Student student) => Create(student);

	public void DeleteStudent(Student student) => Delete(student);

	public async Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges = false) =>
		await FindAll(trackChanges)
			.OrderBy(c => c.Name)
			.ToListAsync();
	public async Task<Student?> GetStudentAsync(Guid studentId, bool trackChanges = false) =>
		await FindByCondition(c => c.StudentId.Equals(studentId), trackChanges)
			.SingleOrDefaultAsync();

	public async Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
		await FindByCondition(x => ids.Contains(x.StudentId), trackChanges)
			.ToListAsync();
}
