using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Courses;

public sealed class CreateCourseHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<CreateCourseCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var entityToCreate = _mapper.Map<Course>(request.Course);

        _rep.Courses.CreateCourse(entityToCreate);
        await _rep.SaveAsync();

        var entityToReturn = _mapper.Map<CourseDto>(entityToCreate);
        return new ApiOkResponse<CourseDto>(entityToReturn);
    }
}
