using System;
using System.Collections.Generic;

namespace LanguageCourses.Domain;

public partial class Student
{
    public Guid StudentId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? MidName { get; set; }

    public DateOnly BirthDate { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

	public virtual ICollection<Debtor> Debtors { get; set; } = [];

    public virtual ICollection<Payment> Payments { get; set; } = [];
}
