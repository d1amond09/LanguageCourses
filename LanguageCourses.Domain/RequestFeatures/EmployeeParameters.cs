using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.RequestFeatures;

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
