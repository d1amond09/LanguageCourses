using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.RequestFeatures.ModelParameters;

public class StudentParameters : RequestParameters
{
    public int MinAge { get; set; } = 0;
    public int MaxAge { get; set; } = int.MaxValue;
    public DateOnly MinBirthDate { get; set; } = new DateOnly(1900,1,1);
    public DateOnly MaxBirthDate { get; set; } = DateOnly.MaxValue;
    public bool NotValidBirthDateRange => MaxBirthDate <= MinBirthDate;
    public bool NotValidAgeRange => MaxAge <= MinAge;
    public Guid? Course { get; set; } = null;
    public string SearchTerm { get; set; } = string.Empty;
    public StudentParameters()
    {
        OrderBy = "surname";
    }
}
