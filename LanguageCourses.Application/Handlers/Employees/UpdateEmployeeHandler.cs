using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Employees;

public sealed class UpdateEmployeeHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<UpdateEmployeeCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var courseEntity = await _rep.Employees.GetEmployeeAsync(request.Id, request.TrackChanges);

        if (courseEntity is null)
            return new EmployeeNotFoundResponse(request.Id);

        _mapper.Map(request.Employee, courseEntity);
        await _rep.SaveAsync();

        return new ApiOkResponse<Employee>(courseEntity);
    }
}
