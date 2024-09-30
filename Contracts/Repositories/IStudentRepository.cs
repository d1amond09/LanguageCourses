using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourses.Domain;

namespace Contracts.Repositories;

public interface IStudentRepository
{
	Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges);
	Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
	Task<Student?> GetStudentAsync(Guid studentId, bool trackChanges);
	public void CreateStudent(Student student);
	public void DeleteStudent(Student student);
}
