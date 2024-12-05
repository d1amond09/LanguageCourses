using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Students;

public sealed class CreateStudentHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<CreateStudentCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var entityToCreate = _mapper.Map<Student>(request.Student);
        entityToCreate.Courses.Clear();

        if (request.Student.CourseIds != null && request.Student.CourseIds.Any())
        {
            var courseIds = request.Student.CourseIds.Distinct().ToList();
            var coursesForStudent = _rep.Courses
                .FindByCondition(x => courseIds.Contains(x.Id))
                .ToList();

            foreach (var course in coursesForStudent)
            {
                _rep.Courses.Attach(course);
                entityToCreate.Courses.Add(course);
            }
        }

        _rep.Students.CreateStudent(entityToCreate);
        await _rep.SaveAsync();

        var entityToReturn = _mapper.Map<StudentDto>(entityToCreate);
        return new ApiOkResponse<StudentDto>(entityToReturn);
    }
}
