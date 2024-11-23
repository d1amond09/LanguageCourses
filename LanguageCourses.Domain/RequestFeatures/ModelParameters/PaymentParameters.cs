using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.RequestFeatures.ModelParameters;

public class PaymentParameters : RequestParameters
{
    public double MinAmount { get; set; } = 0;
    public double MaxAmount { get; set; } = 99999999.99;
    public DateOnly MinDate { get; set; } = new DateOnly(1900,1,1);
    public DateOnly MaxDate { get; set; } = DateOnly.MaxValue;
    public bool NotValidBirthDateRange => MaxAmount <= MinAmount;
    public bool NotValidAgeRange => MaxDate <= MinDate;
    public string SearchTerm { get; set; } = string.Empty;
    public PaymentParameters()
    {
        OrderBy = "date";
    }
}
