using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.RequestFeatures;

public class CourseParameters : RequestParameters
{
    public double MinTuitionFee { get; set; } = 0;
    public double MaxTuitionFee { get; set; } = 999999;
    public bool NotValidTuitionFeeRange => MaxTuitionFee <= MinTuitionFee;
    public string SearchTerm { get; set; } = string.Empty;
    public CourseParameters()
    {
        OrderBy = "name";
    }
}
