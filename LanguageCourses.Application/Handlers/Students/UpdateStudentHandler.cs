using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LanguageCourses.Application.Handlers.Students;

public sealed class UpdateStudentHandler(IRepositoryManager rep, IMapper mapper, ILoggerManager logger) : IRequestHandler<UpdateStudentCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggerManager _logger = logger;

    public async Task<ApiBaseResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var returnEntity = await _rep.Students.GetStudentAsync(request.Id, request.TrackChanges);

        if (returnEntity is null)
            return new StudentNotFoundResponse(request.Id);

        _mapper.Map(request.Student, returnEntity);
        returnEntity.Courses = [];

        var courseIds = request.Student.CourseIds?.Distinct().ToList() ?? [];
        await _rep.Students.UpdateStudentCoursesAsync(returnEntity, courseIds);

        _logger.LogInfo($"{string.Join(',', returnEntity.Courses.Select(x => x.Id))}");

        _rep.Students.UpdateStudent(returnEntity);
        await _rep.SaveAsync();

        _logger.LogInfo($"{string.Join(',', returnEntity.Courses.Select(x => x.Id))}");
        return new ApiOkResponse<Student>(returnEntity);
    }
}