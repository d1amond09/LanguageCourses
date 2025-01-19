using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Employees;

public sealed class DeleteEmployeeHandler(IRepositoryManager rep) : IRequestHandler<DeleteEmployeeCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;

    public async Task<ApiBaseResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _rep.Employees.GetEmployeeAsync(request.Id, request.TrackChanges);

        if (employee is null)
            return new EmployeeNotFoundResponse(request.Id);

        _rep.Employees.DeleteEmployee(employee);
        await _rep.SaveAsync();

        return new ApiOkResponse<Employee>(employee);
    }
}
