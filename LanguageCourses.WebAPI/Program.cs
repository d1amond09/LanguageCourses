
using LanguageCourses.Web.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

namespace LanguageCourses.WebAPI;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		ConfigureServices(builder.Services, builder.Configuration);
		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		ConfigureApp(app);

		app.UseHttpsRedirection();

		app.UseAuthorization();


		app.MapControllers();

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
}
