using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Commands;

public sealed record UpdateStudentCommand(Guid Id, StudentForUpdateDto Student, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

