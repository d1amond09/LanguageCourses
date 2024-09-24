using System;
using System.Collections.Generic;

namespace LanguageCourses.Domain;

public partial class Debtor
{
    public Guid DebtorId { get; set; }

    public decimal DebtAmount { get; set; }

    public DateOnly? LastPaymentDate { get; set; }

    public Guid StudentId { get; set; }

    public virtual Student Student { get; set; } = null!;
}
