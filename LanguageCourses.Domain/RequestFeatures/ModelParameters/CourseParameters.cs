using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.RequestFeatures.ModelParameters;

public class CourseParameters : RequestParameters
{
    public double MinTuitionFee { get; set; } = 0;
    public double MaxTuitionFee { get; set; } = 99999999.99;
    public int MinHours { get; set; } = 0;
    public int MaxHours { get; set; } = int.MaxValue;
    public bool NotValidTuitionFeeRange => MaxTuitionFee <= MinTuitionFee;
    public bool NotValidHoursRange => MaxHours <= MinHours;
    public string SearchTrainingProgram { get; set; } = string.Empty;
    public string SearchTerm { get; set; } = string.Empty;
    public CourseParameters()
    {
        OrderBy = "name";
    }
}
