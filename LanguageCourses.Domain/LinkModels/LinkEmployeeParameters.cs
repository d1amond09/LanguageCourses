using LanguageCourses.Domain.RequestFeatures.ModelParameters;
using Microsoft.AspNetCore.Http;

namespace LanguageCourses.Domain.LinkModels;

public record LinkEmployeeParameters(EmployeeParameters EmployeeParameters, HttpContext Context);
