using System.Text;
using LanguageCourses.Domain.DataTransferObjects;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace LanguageCourses.WebAPI.Formatters.Output;

public class CsvOutputFormatter : TextOutputFormatter
{
	public CsvOutputFormatter()
	{
		SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
		SupportedEncodings.Add(Encoding.UTF8);
		SupportedEncodings.Add(Encoding.Unicode);
	}

	protected override bool CanWriteType(Type? type)
	{
		if (typeof(StudentDto).IsAssignableFrom(type) ||
	   typeof(IEnumerable<StudentDto>).IsAssignableFrom(type))
		{
			return base.CanWriteType(type);
		}
		return false;
	}
	public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
	{
		var response = context.HttpContext.Response;
		var buffer = new StringBuilder();
		if (context.Object is not IEnumerable<StudentDto>)
		{
			ArgumentNullException.ThrowIfNull(context.Object);
			FormatCsv(buffer, (StudentDto)context.Object);
		}
		else
		{
			foreach (var student in (IEnumerable<StudentDto>)context.Object)
			{
				FormatCsv(buffer, student);
			}
		}
		await response.WriteAsync(buffer.ToString());
	}

	private static void FormatCsv(StringBuilder buffer, StudentDto student)
	{
		buffer.AppendLine($"{student.StudentId},\"{student.Surname},\"{student.Name},\"{student.MidName},\"{student.PassportNumber},\"{student.Address},\"{student.BirthDate},\"{student.Phone}\"");
	}
}

