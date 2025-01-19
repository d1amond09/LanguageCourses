namespace LanguageCourses.Domain.RequestFeatures.ModelParameters;

public class EmployeeParameters : RequestParameters
{
    public string Education { get; set; } = string.Empty;
    public Guid? JobTitle { get; set; } = null;
    public string SearchTerm { get; set; } = string.Empty;
    public EmployeeParameters()
    {
        OrderBy = "surname";
    }
}
