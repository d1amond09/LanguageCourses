using LanguageCourses.Domain.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace LanguageCourses.Domain.LinkModels;

public record LinkEmployeeParameters(EmployeeParameters EmployeeParameters, HttpContext Context);
