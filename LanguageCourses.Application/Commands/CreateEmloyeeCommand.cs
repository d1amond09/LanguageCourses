using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Commands;

public sealed record CreateEmployeeCommand(EmployeeForCreationDto Employee) :
    IRequest<ApiBaseResponse>;
