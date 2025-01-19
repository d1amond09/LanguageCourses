using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Queries;

public sealed record GetEmployeeQuery(Guid EmployeeId, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

