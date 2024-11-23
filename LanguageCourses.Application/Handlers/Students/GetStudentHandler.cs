using AutoMapper;
using Contracts;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Students;

public class GetStudentHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<GetStudentQuery, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await _rep.Students.GetStudentAsync(request.StudentId, request.TrackChanges);
        if (student is null)
            return new StudentNotFoundResponse(request.StudentId);

        var studentDto = _mapper.Map<StudentDto>(student);
        return new ApiOkResponse<StudentDto>(studentDto);
    }
}
