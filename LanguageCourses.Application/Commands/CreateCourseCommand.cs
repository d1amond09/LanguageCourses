using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Commands;

public sealed record CreateCourseCommand(CourseForCreationDto Course) :
    IRequest<ApiBaseResponse>;
