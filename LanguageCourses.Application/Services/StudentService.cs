using AutoMapper;
using Contracts;
using Contracts.Services;
using LanguageCourses.Domain.DataTransferObjects;

namespace LanguageCourses.Application.Services;

internal sealed class StudentService(IRepositoryManager rep, ILoggerManager logger, IMapper mapper) : IStudentService
{
    private readonly IRepositoryManager _rep = rep;
    private readonly ILoggerManager _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync(bool trackChanges)
    {
        var students = await _rep.Students.GetAllStudentsAsync(trackChanges);
        var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(students);
        return studentsDto;
    }

    public async Task<StudentDto?> GetStudentAsync(Guid id, bool trackChanges)
    {
        var student = await _rep.Students.GetStudentAsync(id, trackChanges);
        var studentDto = _mapper.Map<StudentDto>(student);
        return studentDto;
    }

}