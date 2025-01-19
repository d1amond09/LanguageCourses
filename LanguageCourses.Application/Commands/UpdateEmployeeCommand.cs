using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Commands;

public sealed record UpdateEmployeeCommand(Guid Id, EmployeeForUpdateDto Employee, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

