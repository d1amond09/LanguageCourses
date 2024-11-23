using LanguageCourses.Domain.RequestFeatures.ModelParameters;
using Microsoft.AspNetCore.Http;

namespace LanguageCourses.Domain.LinkModels;

public record LinkStudentParameters(StudentParameters StudentParameters, HttpContext Context);
