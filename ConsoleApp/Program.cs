using Contracts.Repositories;
using LanguageCourses.Domain;
using LanguageCourses.Persistence;
using LanguageCourses.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp;

internal class Program
{
	static async Task Main()
	{
		var builder = new ConfigurationBuilder();
		builder.SetBasePath(Directory.GetCurrentDirectory());
		builder.AddJsonFile("appsettings.json");
		var config = builder.Build();
		string? connectionString = config.GetConnectionString("localMSSQLConnection");

		var optionsBuilder = new DbContextOptionsBuilder<LanguageCoursesContext>();
		var options = optionsBuilder
			.UseSqlServer(connectionString)
			.Options;

		using (LanguageCoursesContext db = new(options))
		{
			var students = new StudentRepository(db).FindByCondition(x => x.BirthDate > new DateOnly(2004, 12, 1));
			foreach (var u in students.ToList())
			{
				Console.WriteLine($"{u.StudentId} - {u.Name} - {u.BirthDate}");
			}
		}
		Console.Read();

	}
}
