using System;
using System.Collections.Generic;

namespace LanguageCourses.Domain;

public partial class JobTitle
{
    public Guid JobTitleId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public string? Responsibilities { get; set; }

    public string? Requirements { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
