using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Queries;

public sealed record GetEmployeesQuery(LinkEmployeeParameters LinkParameters, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

