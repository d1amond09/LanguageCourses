using LanguageCourses.Domain.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace LanguageCourses.Domain.LinkModels;

public record LinkCourseParameters(CourseParameters CourseParameters, HttpContext Context);
