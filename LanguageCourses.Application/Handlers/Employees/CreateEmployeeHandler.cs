using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Employees;

public sealed class CreateEmployeeHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<CreateEmployeeCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entityToCreate = _mapper.Map<Employee>(request.Employee);

        _rep.Employees.CreateEmployee(entityToCreate);
        await _rep.SaveAsync();

        var entityToReturn = _mapper.Map<EmployeeDto>(entityToCreate);
        return new ApiOkResponse<EmployeeDto>(entityToReturn);
    }
}
