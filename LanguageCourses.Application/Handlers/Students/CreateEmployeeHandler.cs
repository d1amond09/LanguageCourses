using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Students;

public sealed class CreateStudentHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<CreateStudentCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var entityToCreate = _mapper.Map<Student>(request.Student);

        _rep.Students.CreateStudent(entityToCreate);
        await _rep.SaveAsync();

        var entityToReturn = _mapper.Map<StudentDto>(entityToCreate);
        return new ApiOkResponse<StudentDto>(entityToReturn);
    }
}
