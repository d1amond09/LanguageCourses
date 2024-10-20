using System.Collections.Frozen;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Threading.Tasks;
using Contracts.Services;
using LanguageCourses.Application.Services;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LanguageCourses.Web;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		ConfigureServices(builder.Services, builder.Configuration);

		var app = builder.Build();
		if (app.Environment.IsProduction())
			app.UseHsts();

		ConfigureApp(app);

		app.Map("/info", Info);
		app.Map("/table", Table);
		app.Map("/searchform1", SearchForm1);
		app.Map("/searchform2", SearchForm2);

		app.Run(async (context) =>
		{
			IStudentService cachedStudentsService = context.RequestServices.GetService<IStudentService>();
			cachedStudentsService?.AddStudents("Students20");

			string HtmlString = "<HTML><HEAD><TITLE>���������</TITLE></HEAD>" +
			"<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
			"<BODY><H1>�������</H1>";
			HtmlString += "<H2>������ �������� � ��� �������</H2>";
			HtmlString += "<BR><A href='/'>�������</A></BR>";
			HtmlString += "<BR><A href='/table'>���������</A></BR>";
			HtmlString += "<BR><A href='/info'>���������� � �������</A></BR>";
			HtmlString += "<BR><A href='/searchform1'>searchform1</A></BR>";
			HtmlString += "<BR><A href='/searchform2'>searchform2</A></BR>";
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
		services.ConfigureStudentService();

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
			string strResponse = "<HTML><HEAD><TITLE>����������</TITLE></HEAD>" +
			"<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
			"<BODY><H1>����������:</H1>";
			strResponse += "<BR> ������: " + context.Request.Host;
			strResponse += "<BR> ����: " + context.Request.PathBase;
			strResponse += "<BR> ��������: " + context.Request.Protocol;
			strResponse += "<BR><A href='/'>�������</A></BODY></HTML>";
			await context.Response.WriteAsync(strResponse);
		});
	}

	private static void Table(IApplicationBuilder app)
	{
		app.Run(async context =>
		{
			IStudentService? cachedStudentsService = context.RequestServices.GetService<IStudentService>();
			IEnumerable<Student>? students = cachedStudentsService?.GetStudents("Students20");

			string HtmlString = "<HTML><HEAD>" +
				"<TITLE>���������</TITLE></HEAD>" +
				"<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
				"<BODY><H1>������ ����������</H1>" +
				"<TABLE BORDER=1>";
			HtmlString += "<TH>";
			HtmlString += "<TD>�������</TD>";
			HtmlString += "<TD>���</TD>";
			HtmlString += "<TD>�����</TD>";
			HtmlString += "<TD>���� ��������</TD>";
			HtmlString += "<TD>����� ���������</TD>";
			HtmlString += "<TD>����� ��������</TD>";
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
				HtmlString += "</TR>";
			}
			HtmlString += "</TABLE></BODY></HTML>";

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

		IStudentService? cachedStudentsService = context.RequestServices.GetService<IStudentService>();

		cachedStudentsService?.AddStudentsByCondition(
			"Students20",
			x => x.Address.Contains(searchData.City) &&
				  x.Name.Contains(searchData.Name));
		var students = cachedStudentsService?.GetStudents("Students20");

		context.Response.Cookies.Append("searchData", JsonSerializer.Serialize(searchData), new CookieOptions
		{
			Expires = DateTimeOffset.UtcNow.AddDays(30)
		});

		string tableHtml = "<TABLE BORDER=1>";
		tableHtml += "<TH><TD>�������</TD><TD>���</TD><TD>�����</TD><TD>���� ��������</TD><TD>����� ��������</TD><TD>����� ��������</TD></TH>";

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

		string formHtml = "<HTML><HEAD><TITLE>����� ������ 2</TITLE></HEAD>" +
			"<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
			"<BODY>" +
			"<FORM method='get' action='/searchform2'>" +
			"����� �� �����:<BR><INPUT type='text' name='name' value='" + searchData.Name + "'>" +
			"<BR>�������� �����:<BR><SELECT name='city'>" +
			"<OPTION value='������'" + (selectedCity == "������" ? " selected" : "") + ">������</OPTION>" +
			"<OPTION value='�����'" + (selectedCity == "�����" ? " selected" : "") + ">�����</OPTION>" +
			"</SELECT><BR><BR><INPUT type='submit' value='������'>" +
			"<INPUT type='button' value='��������' onclick='alert(\"" + searchData.Name + " " + searchData.City + "\");'></FORM>" +
			"<BR><A href='/'>�������</A>" +
			"<H2>���������� ������:</H2>" +
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

		IStudentService? cachedStudentsService = context.RequestServices.GetService<IStudentService>();
		
		cachedStudentsService?.AddStudentsByCondition(
			"Students20",
			x => x.Address.Contains(searchData.City) &&
					x.Name.Contains(searchData.Name));
		var students = cachedStudentsService?.GetStudents("Students20");

		context.Session.SetString("searchData", JsonSerializer.Serialize(searchData));

		string tableHtml = "<TABLE BORDER=1>";
		tableHtml += "<TH><TD>�������</TD><TD>���</TD><TD>�����</TD><TD>���� ��������</TD><TD>����� ��������</TD><TD>����� ��������</TD></TH>";

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

		string formHtml = "<HTML><HEAD><TITLE>����� ������ 2</TITLE></HEAD>" +
		"<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
		"<BODY>" +
		"<FORM method='get' action='/searchform2'>" +
		"����� �� �����:<BR><INPUT type='text' name='name' value='" + searchData.Name + "'>" +
		"<BR>�������� �����:<BR><SELECT name='city'>" +
		"<OPTION value='������'" + (selectedCity == "������" ? " selected" : "") + ">������</OPTION>" +
		"<OPTION value='�����'" + (selectedCity == "�����" ? " selected" : "") + ">�����</OPTION>" +
		"</SELECT><BR><BR><INPUT type='submit' value='������'>" +
		"<INPUT type='button' value='��������' onclick='alert(\"" + searchData.Name + " " + searchData.City + "\");'></FORM>" +
		"<BR><A href='/'>�������</A>" +
		"<H2>���������� ������:</H2>" +
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

