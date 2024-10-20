using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.Entities.DataTransferObjects;

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
}
