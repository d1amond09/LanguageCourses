using LanguageCourses.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp;

internal class Program
{
	static void Main()
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

		using (LanguageCoursesContext db = new (options))
		{
			var users = db.Employees.ToList();
			foreach (var u in users)
			{
				Console.WriteLine($"{u.EmployeeId} - {u.Name} - {u.Address}");
			}
		}
		Console.Read();

	}
}
