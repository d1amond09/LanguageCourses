﻿using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Domain.DataTransferObjects;

public record StudentDto
{
	public Guid StudentId { get; init; } = new();

	public string Surname { get; init; } = null!;

	public string Name { get; init; } = null!;

	public string? MidName { get; init; }

	public DateOnly BirthDate { get; init; }

	public string Address { get; init; } = null!;

	public string Phone { get; init; } = null!;

	public string PassportNumber { get; init; } = null!;

    public ICollection<PaymentDto>? Payments { get; set; } 

    public ICollection<CourseDto>? Courses { get; set; } 

    public override string ToString()
    {
        return $"{Surname} {Name} {MidName}";
    }
}
