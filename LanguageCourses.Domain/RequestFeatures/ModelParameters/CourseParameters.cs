namespace LanguageCourses.Domain.RequestFeatures.ModelParameters;

public class CourseParameters : RequestParameters
{
    public double MinTuitionFee { get; set; } = minZeroValue;
    public double MaxTuitionFee { get; set; } = maxDoubleValue;
    public int MinHours { get; set; } = minZeroValue;
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
