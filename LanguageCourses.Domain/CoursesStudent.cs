using System;
using System.Collections.Generic;

namespace LanguageCourses.Domain;

public partial class CoursesStudent
{
    public Guid CourseId { get; set; }

    public Guid StudentId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
