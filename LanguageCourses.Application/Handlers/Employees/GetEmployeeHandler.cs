using AutoMapper;
using Contracts;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Employees;

public class GetEmployeeHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<GetEmployeeQuery, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var course = await _rep.Employees.GetEmployeeAsync(request.EmployeeId, request.TrackChanges);
        if (course is null)
            return new EmployeeNotFoundResponse(request.EmployeeId);

        var courseDto = _mapper.Map<EmployeeDto>(course);
        return new ApiOkResponse<EmployeeDto>(courseDto);
    }
}
