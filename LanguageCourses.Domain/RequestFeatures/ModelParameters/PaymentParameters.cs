using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.RequestFeatures.ModelParameters;

public class PaymentParameters : RequestParameters
{
    public double MinAmount { get; set; } = minZeroValue;
    public double MaxAmount { get; set; } = maxDoubleValue;
    public DateOnly MinDate { get; set; } = new DateOnly(minYear, minMonth, minDay);
    public DateOnly MaxDate { get; set; } = DateOnly.MaxValue;
    public bool NotValidDateRange => MaxAmount <= MinAmount;
    public bool NotValidAmountRange => MaxDate <= MinDate;
    public string SearchTerm { get; set; } = string.Empty;
    public PaymentParameters()
    {
        OrderBy = "date";
    }
}
