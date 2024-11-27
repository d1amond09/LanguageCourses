using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.RequestFeatures;

public abstract class RequestParameters
{
    protected const double maxDoubleValue = 99999999.99;
    protected const int minZeroValue = 0;
    protected const int minYear = 1900;
    protected const int minMonth = 1;
    protected const int minDay = 1;

    const int maxPageSize = 2000;
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value > maxPageSize ?
                maxPageSize : value;
        }
    }
    public string OrderBy { get; set; } = string.Empty;
    public string Fields { get; set; } = string.Empty;
}


