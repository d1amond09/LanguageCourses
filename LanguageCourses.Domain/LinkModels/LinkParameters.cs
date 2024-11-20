using LanguageCourses.Domain.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace LanguageCourses.Domain.LinkModels;

public record LinkParameters(CourseParameters CourseParameters, HttpContext Context);
