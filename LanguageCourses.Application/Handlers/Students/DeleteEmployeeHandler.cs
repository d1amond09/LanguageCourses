using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Students;

public sealed class DeleteStudentHandler(IRepositoryManager rep) : IRequestHandler<DeleteStudentCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;

    public async Task<ApiBaseResponse> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _rep.Students.GetStudentAsync(request.Id, request.TrackChanges);

        if (student is null)
            return new StudentNotFoundResponse(request.Id);

        _rep.Students.DeleteStudent(student);
        await _rep.SaveAsync();

        return new ApiOkResponse<Student>(student);
    }
}
