using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Students;

public sealed class UpdateStudentHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<UpdateStudentCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var returnEntity = await _rep.Students.GetStudentAsync(request.Id, request.TrackChanges);

        if (returnEntity is null)
            return new StudentNotFoundResponse(request.Id);

        _mapper.Map(request.Student, returnEntity);
        await _rep.SaveAsync();

        return new ApiOkResponse<Student>(returnEntity);
    }
}
