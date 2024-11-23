using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.RequestFeatures.ModelParameters;

public class JobTitleParameters : RequestParameters
{
    public double MinSalary { get; set; } = minZeroValue;
    public double MaxSalary { get; set; } = maxDoubleValue;
    public bool NotValidSalaryRange => MaxSalary <= MinSalary;
    public string SearchTerm { get; set; } = string.Empty;
    public JobTitleParameters()
    {
        OrderBy = "name";
    }
}
