namespace LanguageCourses.Domain.Exceptions;

public sealed class ValidationAppException(IReadOnlyDictionary<string, string[]> errors) :
    Exception("One or more validation errors occurred")
{
    public IReadOnlyDictionary<string, string[]> Errors { get; } = errors;
}
