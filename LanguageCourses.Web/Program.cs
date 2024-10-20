using System.Text.Json;
using Contracts.Services;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Web.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

namespace LanguageCourses.Web;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		ConfigureServices(builder.Services, builder.Configuration);

		var app = builder.Build();
		if (app.Environment.IsProduction())
			app.UseHsts();

		ConfigureApp(app);

		app.Map("/info", Info);
		app.Map("/students", Students);
		app.Map("/employees", Employees);
		app.Map("/courses", Courses);
		app.Map("/payments", Payments);
		app.Map("/jobtitles", JobTitles);
		app.Map("/searchform1", SearchForm1);
		app.Map("/searchform2", SearchForm2);

		app.Run(async (context) =>
		{
			IStudentService cachedStudentsService = context.RequestServices.GetService<IStudentService>();
			cachedStudentsService?.AddStudents("Students20");

			string HtmlString = "<HTML><HEAD><TITLE>Главная</TITLE></HEAD>" +
			"<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
			"<BODY><H1>Главная</H1>";
			HtmlString += "<H2>Данные записаны в кэш сервера</H2>";
			HtmlString += "<BR><A href='/'>Главная</A>";
			HtmlString += "<BR><A href='/students'>Слушатели</A>";
			HtmlString += "<BR><A href='/employees'>Работники</A>";
			HtmlString += "<BR><A href='/courses'>Курсы</A>";
			HtmlString += "<BR><A href='/payments'>Платежи</A>";
			HtmlString += "<BR><A href='/jobtitles'>Должности</A>";
			HtmlString += "<BR><A href='/info'>Информация о клиенте</A>";
			HtmlString += "<BR><A href='/searchform1'>searchform1</A>";
			HtmlString += "<BR><A href='/searchform2'>searchform2</A>";
			HtmlString += "</BODY></HTML>";

			await context.Response.WriteAsync(HtmlString);
		});

		app.Run();
	}

	public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		services.ConfigureCors();

		services.ConfigureSqlContext(configuration);

		services.AddMemoryCache();
		services.AddDistributedMemoryCache();
		services.AddSession();

		services.ConfigureRepositoryManager();
		services.ConfigureServiceManager();
	}

	public static void ConfigureApp(IApplicationBuilder app)
	{
		app.UseHttpsRedirection();

		app.UseForwardedHeaders(new ForwardedHeadersOptions
		{
			ForwardedHeaders = ForwardedHeaders.All
		});

		app.UseCors("CorsPolicy");
		app.UseSession();
		app.UseCookiePolicy();
	}

	private static void Info(IApplicationBuilder app)
	{
		app.Run(async (context) =>
		{
			string strResponse = "<HTML><HEAD><TITLE>Информация</TITLE></HEAD>" +
			"<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
			"<BODY><H1>Информация:</H1>";
			strResponse += "<BR> Сервер: " + context.Request.Host;
			strResponse += "<BR> Путь: " + context.Request.PathBase;
			strResponse += "<BR> Протокол: " + context.Request.Protocol;
			strResponse += "<BR><A href='/'>Главная</A></BODY></HTML>";
			await context.Response.WriteAsync(strResponse);
		});
	}

	private static void Students(IApplicationBuilder app)
	{
		app.Run(async context =>
		{
			IStudentService? cachedStudentsService = context.RequestServices.GetService<IServiceManager>()?.StudentService;
			IEnumerable<Student>? students = cachedStudentsService?.GetStudents("Students20");

			string HtmlString = "<HTML><HEAD>" +
				"<TITLE>Слушатели</TITLE>" +
				"<META http-equiv='Content-Type' content='text/html; charset=utf-8 >'" +
				"</HEAD><BODY><H1>Список слушателей</H1>" +
				"<TABLE BORDER=1 cellspacing=0>";
			HtmlString += "<TH>";
			HtmlString += "<TD>Фамилия</TD>";
			HtmlString += "<TD>Имя</TD>";
			HtmlString += "<TD>Адрес</TD>";
			HtmlString += "<TD>Дата рождения</TD>";
			HtmlString += "<TD>Номер пасспорта</TD>";
			HtmlString += "<TD>Номер телефона</TD>";
			HtmlString += "<TD>Названия курсов</TD>";
			HtmlString += "<TD>Оплаты</TD>";
			HtmlString += "</TH>";
			foreach (Student student in students)
			{
				HtmlString += "<TR>";
				HtmlString += "<TD>" + student.StudentId + "</TD>";
				HtmlString += "<TD>" + student.Surname + "</TD>";
				HtmlString += "<TD>" + student.Name + "</TD>";
				HtmlString += "<TD>" + student.Address + "</TD>";
				HtmlString += "<TD>" + student.BirthDate + "</TD>";
				HtmlString += "<TD>" + student.PassportNumber + "</TD>";
				HtmlString += "<TD>" + student.Phone + "</TD>";
				HtmlString += "<TD>";
				foreach (string courseName in student.Courses.Select(x => x.Name))
					HtmlString += courseName + "<BR>";
				HtmlString += "</TD>";
				HtmlString += "<TD>";
				foreach (string pay in student.Payments.Select(x => x.Purpose + ": " + x.Amount))
					HtmlString += pay + "<BR>";
				HtmlString += "</TD>";
				HtmlString += "</TR>";
			}
			HtmlString += "</table></BODY></HTML>";

			await context.Response.WriteAsync(HtmlString);
		});
	}

	private static void Employees(IApplicationBuilder app)
	{
		app.Run(async context =>
		{
			IEmployeeService? cachedEmployeesService = context.RequestServices.GetService<IServiceManager>()?.EmployeeService;
			IEnumerable<Employee>? employees = cachedEmployeesService?.GetEmployees("Employees20");

			string HtmlString = "<HTML><HEAD>" +
				"<TITLE>Сотрудники</TITLE></HEAD>" +
				"<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
				"<BODY><H1>Список сотрудников</H1>" +
				"<TABLE BORDER=1 cellspacing=0>";
			HtmlString += "<TH>";
			HtmlString += "<TD>Должность</TD>";
			HtmlString += "<TD>Фамилия</TD>";
			HtmlString += "<TD>Имя</TD>";
			HtmlString += "<TD>Адрес</TD>";
			HtmlString += "<TD>Дата рождения</TD>";
			HtmlString += "<TD>Номер пасспорта</TD>";
			HtmlString += "<TD>Номер телефона</TD>";
			HtmlString += "<TD>Образование</TD>";
			HtmlString += "<TD>Названия курсов</TD>";
			HtmlString += "</TH>";
			foreach (Employee employee in employees)
			{
				HtmlString += "<TR>";
				HtmlString += "<TD>" + employee.EmployeeId + "</TD>";
				HtmlString += "<TD>" + employee.JobTitle.Name + "</TD>";
				HtmlString += "<TD>" + employee.Surname + "</TD>";
				HtmlString += "<TD>" + employee.Name + "</TD>";
				HtmlString += "<TD>" + employee.Address + "</TD>";
				HtmlString += "<TD>" + employee.BirthDate + "</TD>";
				HtmlString += "<TD>" + employee.PassportNumber + "</TD>";
				HtmlString += "<TD>" + employee.Phone + "</TD>";
				HtmlString += "<TD>" + employee.Education + "</TD>";
				HtmlString += "<TD>";
				foreach (string courseName in employee.Courses.Select(x => x.Name))
					HtmlString += courseName + "<BR>";
				HtmlString += "</TD>";
				HtmlString += "</TR>";
			}
			HtmlString += "</table></BODY></HTML>";

			await context.Response.WriteAsync(HtmlString);
		});
	}

	private static void Courses(IApplicationBuilder app)
	{
		app.Run(async context =>
		{
			ICourseService? cachedCoursesService = context.RequestServices.GetService<IServiceManager>()?.CourseService;
			IEnumerable<Course>? сourses = cachedCoursesService?.GetCourses("Courses20");

			string HtmlString = "<HTML><HEAD>" +
				"<TITLE>Курсы</TITLE></HEAD>" +
				"<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
				"<BODY><H1>Список курсов</H1>" +
				"<TABLE BORDER=1 cellspacing=0>";
			HtmlString += "<TH>";
			HtmlString += "<TD>Название курсов</TD>";
			HtmlString += "<TD>Стоимость обучения</TD>";
			HtmlString += "<TD>Доступные места</TD>";
			HtmlString += "<TD>Часы</TD>";
			HtmlString += "<TD>Интенсивность</TD>";
			HtmlString += "<TD>Программа</TD>";
			HtmlString += "<TD>Преподаватель</TD>";
			HtmlString += "<TD>Слушатели</TD>";
			HtmlString += "</TH>";
			foreach (Course course in сourses)
			{
				HtmlString += "<TR>";
				HtmlString += "<TD>" + course.CourseId + "</TD>";
				HtmlString += "<TD>" + course.Name + "</TD>";
				HtmlString += "<TD>" + course.TuitionFee + "</TD>";
				HtmlString += "<TD>" + course.AvailableSeats + "</TD>";
				HtmlString += "<TD>" + course.Hours + "</TD>";
				HtmlString += "<TD>" + course.Intensity + "</TD>";
				HtmlString += "<TD>" + course.TrainingProgram + "</TD>";
				HtmlString += "<TD>" + course.Employee.Surname + " " + course.Employee.Name + "</TD>";
				HtmlString += "<TD>";
				foreach (string courseName in course.Students.Select(x => x.Surname + " " + x.Name + " " + x.Address))
					HtmlString += courseName + "<BR>";
				HtmlString += "</TD>";
				HtmlString += "</TR>";
			}
			HtmlString += "</table></BODY></HTML>";

			await context.Response.WriteAsync(HtmlString);
		});
	}

	private static void Payments(IApplicationBuilder app)
	{
		app.Run(async context =>
		{
			IPaymentService? cachedPaymentsService = context.RequestServices.GetService<IServiceManager>()?.PaymentService;
			IEnumerable<Payment>? payments = cachedPaymentsService?.GetPayments("Payments20");

			string HtmlString = "<HTML><HEAD>" +
				"<TITLE>Платежи</TITLE></HEAD>" +
				"<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
				"<BODY><H1>Список платежей</H1>" +
				"<TABLE BORDER=1 cellspacing=0>";
			HtmlString += "<TH>";
			HtmlString += "<TD>Название платежа</TD>";
			HtmlString += "<TD>Дата</TD>";
			HtmlString += "<TD>Сумма</TD>";
			HtmlString += "<TD>Слушатель</TD>";
			HtmlString += "</TH>";
			foreach (Payment payment in payments)
			{
				HtmlString += "<TR>";
				HtmlString += "<TD>" + payment.PaymentId + "</TD>";
				HtmlString += "<TD>" + payment.Purpose + "</TD>";
				HtmlString += "<TD>" + payment.Date + "</TD>";
				HtmlString += "<TD>" + payment.Amount + "</TD>";
				HtmlString += "<TD>" + payment.Student.Surname + " " + payment.Student.Name + "</TD>";
				HtmlString += "</TR>";
			}
			HtmlString += "</table></BODY></HTML>";

			await context.Response.WriteAsync(HtmlString);
		});
	}

	private static void JobTitles(IApplicationBuilder app)
	{
		app.Run(async context =>
		{
			IJobTitleService? cachedJobTitlesService = context.RequestServices.GetService<IServiceManager>()?.JobTitleService;
			IEnumerable<JobTitle>? jobTitles = cachedJobTitlesService?.GetJobTitles("JobTitles20");

			string HtmlString = "<HTML><HEAD>" +
				"<TITLE>Должности</TITLE></HEAD>" +
				"<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
				"<BODY><H1>Список должностей</H1>" +
				"<TABLE BORDER=1 cellspacing=0>";
			HtmlString += "<TH>";
			HtmlString += "<TD>Название должности</TD>";
			HtmlString += "<TD>Зарплата</TD>";
			HtmlString += "<TD>Сотрудник</TD>";
			HtmlString += "</TH>";
			foreach (JobTitle jobTitle in jobTitles)
			{
				HtmlString += "<TR>";
				HtmlString += "<TD>" + jobTitle.JobTitleId + "</TD>";
				HtmlString += "<TD>" + jobTitle.Name + "</TD>";
				HtmlString += "<TD>" + jobTitle.Salary + "</TD>";
				HtmlString += "<TD>";
				foreach (string empl in jobTitle.Employees.Select(x => x.Surname + " " + x.Name + " " + x.Address))
					HtmlString += empl + "<BR>";
				HtmlString += "</TD>";


				HtmlString += "</TR>";
			}
			HtmlString += "</table></BODY></HTML>";

			await context.Response.WriteAsync(HtmlString);
		});
	}

	private static void SearchForm1(IApplicationBuilder app) =>
		app.Run(HandleSearchForm1);

	private static async Task HandleSearchForm1(HttpContext context)
	{
		var userJson = context.Request.Cookies["searchData"];
		var searchData = string.IsNullOrEmpty(userJson) ? new SearchData() : JsonSerializer.Deserialize<SearchData>(userJson);

		ArgumentNullException.ThrowIfNull(searchData);

		if (context.Request.Query.ContainsKey("name"))
		{
			searchData.Name = context.Request.Query["name"];
		}
		if (context.Request.Query.ContainsKey("city"))
		{
			searchData.City = context.Request.Query["city"];
		}

		IStudentService? cachedStudentsService = context.RequestServices.GetService<IServiceManager>()?.StudentService;

		cachedStudentsService?.AddStudentsByCondition(
			"Students20",
			x => x.Address.Contains(searchData.City) &&
				  x.Name.Contains(searchData.Name));
		var students = cachedStudentsService?.GetStudents("Students20");

		context.Response.Cookies.Append("searchData", JsonSerializer.Serialize(searchData), new CookieOptions
		{
			Expires = DateTimeOffset.UtcNow.AddDays(30)
		});

		string tableHtml = "<TABLE BORDER=1 cellspacing=0>";
		tableHtml += "<TH><TD>Фамилия</TD><TD>Имя</TD><TD>Адрес</TD><TD>Дата рождения</TD><TD>Номер паспорта</TD><TD>Номер телефона</TD></TH>";

		foreach (Student student in students ?? Enumerable.Empty<Student>())
		{
			tableHtml += "<TR>";
			tableHtml += $"<TD>{student.StudentId}</TD>";
			tableHtml += $"<TD>{student.Surname}</TD>";
			tableHtml += $"<TD>{student.Name}</TD>";
			tableHtml += $"<TD>{student.Address}</TD>";
			tableHtml += $"<TD>{student.BirthDate:dd-MM-yyyy}</TD>";
			tableHtml += $"<TD>{student.PassportNumber}</TD>";
			tableHtml += $"<TD>{student.Phone}</TD>";
			tableHtml += "</TR>";
		}
		tableHtml += "</TABLE>";

		string selectedCity = searchData.City ?? string.Empty;

		string formHtml = "<HTML><HEAD><TITLE>Форма поиска 2</TITLE></HEAD>" +
			"<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
			"<BODY>" +
			"<FORM method='get' action='/searchform2'>" +
			"Поиск по Имени:<BR><INPUT type='text' name='name' value='" + searchData.Name + "'>" +
			"<BR>Выберите город:<BR><SELECT name='city'>" +
			"<OPTION value='Гомель'" + (selectedCity == "Гомель" ? " selected" : "") + ">Гомель</OPTION>" +
			"<OPTION value='Минск'" + (selectedCity == "Минск" ? " selected" : "") + ">Минск</OPTION>" +
			"</SELECT><BR><BR><INPUT type='submit' value='Искать'>" +
			"<INPUT type='button' value='Показать' onclick='alert(\"" + searchData.Name + " " + searchData.City + "\");'></FORM>" +
			"<BR><A href='/'>Главная</A>" +
			"<H2>Результаты поиска:</H2>" +
			tableHtml +
			"</BODY></HTML>";

		await context.Response.WriteAsync(formHtml);
	}

	private static void SearchForm2(IApplicationBuilder app) =>
		app.Run(HandleSearchForm2);
	private static async Task HandleSearchForm2(HttpContext context)
	{
		var userJson = context.Session.GetString("searchData");
		var searchData = string.IsNullOrEmpty(userJson) ? new SearchData() : JsonSerializer.Deserialize<SearchData>(userJson);

		ArgumentNullException.ThrowIfNull(searchData);

		if (context.Request.Query.ContainsKey("name"))
		{
			searchData.Name = context.Request.Query["name"];
		}
		if (context.Request.Query.ContainsKey("city"))
		{
			searchData.City = context.Request.Query["city"];
		}

		IStudentService? cachedStudentsService = context.RequestServices.GetService<IServiceManager>()?.StudentService;

		cachedStudentsService?.AddStudentsByCondition(
			"Students20",
			x => x.Address.Contains(searchData.City) &&
					x.Name.Contains(searchData.Name));
		var students = cachedStudentsService?.GetStudents("Students20");

		context.Session.SetString("searchData", JsonSerializer.Serialize(searchData));

		string tableHtml = "<TABLE BORDER=1 cellspacing=0>";
		tableHtml += "<TH><TD>Фамилия</TD><TD>Имя</TD><TD>Адрес</TD><TD>Дата рождения</TD><TD>Номер паспорта</TD><TD>Номер телефона</TD></TH>";

		foreach (Student student in students ?? [])
		{
			tableHtml += "<TR>";
			tableHtml += $"<TD>{student.StudentId}</TD>";
			tableHtml += $"<TD>{student.Surname}</TD>";
			tableHtml += $"<TD>{student.Name}</TD>";
			tableHtml += $"<TD>{student.Address}</TD>";
			tableHtml += $"<TD>{student.BirthDate:dd-MM-yyyy}</TD>";
			tableHtml += $"<TD>{student.PassportNumber}</TD>";
			tableHtml += $"<TD>{student.Phone}</TD>";
			tableHtml += "</TR>";
		}
		tableHtml += "</TABLE>";

		string selectedCity = searchData.City ?? string.Empty;

		string formHtml = "<HTML><HEAD><TITLE>Форма поиска 2</TITLE></HEAD>" +
		"<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
		"<BODY>" +
		"<FORM method='get' action='/searchform2'>" +
		"Поиск по Имени:<BR><INPUT type='text' name='name' value='" + searchData.Name + "'>" +
		"<BR>Выберите город:<BR><SELECT name='city'>" +
		"<OPTION value='Гомель'" + (selectedCity == "Гомель" ? " selected" : "") + ">Гомель</OPTION>" +
		"<OPTION value='Минск'" + (selectedCity == "Минск" ? " selected" : "") + ">Минск</OPTION>" +
		"</SELECT><BR><BR><INPUT type='submit' value='Искать'>" +
		"<INPUT type='button' value='Показать' onclick='alert(\"" + searchData.Name + " " + searchData.City + "\");'></FORM>" +
		"<BR><A href='/'>Главная</A>" +
		"<H2>Результаты поиска:</H2>" +
		tableHtml +
		"</BODY></HTML>";

		await context.Response.WriteAsync(formHtml);
	}
}

public class SearchData
{
	public string Name { get; set; }
	public string City { get; set; }
}

