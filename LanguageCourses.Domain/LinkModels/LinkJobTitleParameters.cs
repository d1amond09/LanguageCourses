using LanguageCourses.Domain.RequestFeatures.ModelParameters;
using Microsoft.AspNetCore.Http;

namespace LanguageCourses.Domain.LinkModels;

public record LinkJobTitleParameters(JobTitleParameters JobTitleParameters, HttpContext Context);
