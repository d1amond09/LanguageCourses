using Contracts;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Persistence;
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
			IRepositoryManager repManager = new RepositoryManager(db);
			await Select1(repManager);
			await Select2(repManager);
			await Select3(repManager);
			await Select4(repManager);
			await Select5(repManager);

			Insert1(repManager);
			Delete1(repManager);

			Insert2(repManager);

			UpdateStudent(repManager);

			Delete2(repManager);



		}
		Console.Read();
	}

	public static async Task Select1(IRepositoryManager repManager)
	{
		Console.WriteLine("Выборка всех данных из таблицы JobTitles");

		var jobTitles = await repManager.JobTitles.GetAllJobTitlesAsync(false);
		foreach (JobTitle j in jobTitles.ToList())
		{
			Console.WriteLine($"{j.Name}: {j.Salary}");
		}

		Console.WriteLine("\nНажмите для продолжения...");
		Console.Read();
	}

	public static async Task Select2(IRepositoryManager repManager)
	{
		decimal salary = 2900;
		Console.WriteLine($"Выборка должностей из таблицы JobTitles, зарплата которых больше {salary}");

		var jobTitles = await repManager.JobTitles.GetJobTitlesWithSalaryMoreThanAsync(salary);

		foreach (JobTitle jt in jobTitles)
		{
			Console.WriteLine($"{jt.Name}: {jt.Salary}");
		}

		Console.WriteLine("\nНажмите для продолжения...");
		Console.Read();
	}

	public static async Task Select3(IRepositoryManager repManager)
	{
		Console.WriteLine($"Выборка целей платежей и средней суммы оплаты из таблицы Payments");

		var groups = await repManager.Payments.GetPaymentsByPurposeAsync();
		foreach (var (Purpose, AvgAmount) in groups)
		{
			Console.WriteLine($"Цель платежа:{Purpose}, Средняя сумма оплаты: {AvgAmount}");
		}

		Console.WriteLine("\nНажмите для продолжения...");
		Console.Read();
	}

	public static async Task Select4(IRepositoryManager repManager)
	{
		Console.WriteLine($"Выборка сотрудников и их должностей из таблиц JobTitles и Employees");

		var groups = await repManager.Employees.GetEmployeesJobtitlesAsync();
		foreach (var (jobTitle, employee) in groups)
		{
			Console.WriteLine($"Должность:{jobTitle.Name}, Сотрудник: {employee.Surname} {employee.Name} {employee.Address}");
		}

		Console.WriteLine("\nНажмите для продолжения...");
		Console.Read();
	}


	public static async Task Select5(IRepositoryManager repManager)
	{
		decimal salary = 2900;
		Console.WriteLine($"Выборка сотрудников и их должностей из таблиц JobTitles и Employees, зарплата которых больше {salary}");

		var groups = await repManager.Employees.GetEmployeesJobtitlesWithFilterSalaryAsync(salary);
		foreach (var (jobTitle, employee) in groups)
		{
			Console.WriteLine($"Должность:{jobTitle.Name} Зарплата:{jobTitle.Salary}, Сотрудник: {employee.Surname} {employee.Name} {employee.Address}");
		}

		Console.WriteLine("\nНажмите для продолжения...");
		Console.Read();
	}

	public static void Insert1(IRepositoryManager repManager)
	{

		JobTitle? jobTitle1 = new()
		{
			Name = "Репетитор",
			Salary = 2500,
			Requirements = "Знания требуемой области"
		};

		Console.WriteLine($"Вставка должности {jobTitle1.Name} с зарплатой {jobTitle1.Salary} ");

		repManager.JobTitles.CreateJobTitle(jobTitle1);
		repManager.SaveChanges();

		JobTitle? jobTitle2 = repManager.JobTitles.GetJobTitleByName("Репетитор");

		Console.WriteLine($"Должность: {jobTitle2?.Name} {jobTitle2?.Salary} {jobTitle2?.Requirements}");

		Console.WriteLine("\nНажмите для продолжения...");
		Console.Read();
	}

	public static void Insert2(IRepositoryManager repManager)
	{
		Student student1 = new()
		{
			Name = "Максим",
			Surname = "Бердников",
			MidName = "Михайлович",
			Address = "ул. Героев-подпольщиков д. 23, кв. 146",
			BirthDate = new DateOnly(2004, 10, 31),
			PassportNumber = "HB3232156",
			Phone = "+375445464319"
		};

		Console.WriteLine($"Вставка слушателя курса с номером паспорта {student1.PassportNumber}");

		repManager.Students.CreateStudent(student1);
		repManager.SaveChanges();

		Student? student2 = repManager.Students.GetStudentByPassport("HB3232156");

		Console.WriteLine($"Слушатель курса: {student2?.Surname} {student2?.Name} {student2?.MidName} {student2?.Address} {student2?.BirthDate} {student2?.PassportNumber} {student2?.Phone}");

		Console.WriteLine("\nНажмите для продолжения...");
		Console.Read();
	}

	public static void UpdateStudent(IRepositoryManager repManager)
	{
		Student? student2 = repManager.Students.GetStudentByPassport("HB3232156", true);
		Console.WriteLine($"Слушатель курса: {student2?.Surname} {student2?.Name} {student2?.MidName} {student2?.Address} {student2?.BirthDate} {student2?.PassportNumber} {student2?.Phone}");

		ArgumentNullException.ThrowIfNull(student2);

		student2.Surname = "Дмитриев";
		Console.WriteLine($"Обновление слушателя курса с номером паспорта {student2.PassportNumber}");

		repManager.Students.UpdateStudent(student2);
		repManager.SaveChanges();


		student2 = repManager.Students.GetStudentByPassport("HB3232156", true);
		Console.WriteLine($"Слушатель курса: {student2?.Surname} {student2?.Name} {student2?.MidName} {student2?.Address} {student2?.BirthDate} {student2?.PassportNumber} {student2?.Phone}");

		Console.WriteLine("\nНажмите для продолжения...");
		Console.Read();
	}

	public static void Delete2(IRepositoryManager repManager)
	{
		Student? student2 = repManager.Students.GetStudentByPassport("HB3232156", true);
		if (student2 == null)
		{
			Console.WriteLine("! Удаление не произошло !");
			Console.WriteLine("\nНажмите для продолжения...");
			Console.Read();
		}

		Console.WriteLine($"Слушатель курса: {student2?.Surname} {student2?.Name} {student2?.MidName} {student2?.Address} {student2?.BirthDate} {student2?.PassportNumber} {student2?.Phone}");

		Console.WriteLine($"Удаление слушателя курса с номером паспорта {student2?.PassportNumber}");

		ArgumentNullException.ThrowIfNull(student2);
		repManager.Students.DeleteStudent(student2);
		repManager.SaveChanges();

		Console.WriteLine("\nНажмите для продолжения...");
		Console.Read();
	}


	public static void Delete1(IRepositoryManager repManager)
	{
		JobTitle? jobTitle2 = repManager.JobTitles.GetJobTitleByName("Репетитор");
		if (jobTitle2 == null)
		{
			Console.WriteLine("! Удаление не произошло !");
			Console.WriteLine("\nНажмите для продолжения...");
			Console.Read();
			return;
		}
		Console.WriteLine($"Должность: {jobTitle2?.Name} {jobTitle2?.Salary} {jobTitle2?.Requirements}");

		Console.WriteLine($"Удаление должности c названием {jobTitle2?.Name}");

		ArgumentNullException.ThrowIfNull(jobTitle2);
		repManager.JobTitles.DeleteJobTitle(jobTitle2);
		repManager.SaveChanges();

		Console.WriteLine("\nНажмите для продолжения...");
		Console.Read();
	}

}
