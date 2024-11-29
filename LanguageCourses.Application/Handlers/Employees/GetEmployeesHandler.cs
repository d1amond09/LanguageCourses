using AutoMapper;
using Contracts;
using Contracts.ModelLinks;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Employees;

public class GetEmployeesHandler(IRepositoryManager rep, IMapper mapper, IEmployeeLinks courseLinks) :
    IRequestHandler<GetEmployeesQuery, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;
    private readonly IEmployeeLinks _courseLinks = courseLinks;

    public async Task<ApiBaseResponse> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var coursesWithMetaData = await _rep.Employees.GetAllEmployeesAsync(
            request.LinkParameters.EmployeeParameters,
            request.TrackChanges
        );

        var coursesDto = _mapper.Map<IEnumerable<EmployeeDto>>(coursesWithMetaData);

        var links = _courseLinks.TryGenerateLinks(
            coursesDto,
            request.LinkParameters.EmployeeParameters.Fields,
            request.LinkParameters.Context
        );

        (LinkResponse, MetaData) result = new(links, coursesWithMetaData.MetaData);

        return new ApiOkResponse<(LinkResponse, MetaData)>(result);
    }
}
