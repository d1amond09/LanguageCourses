using System;
using Contracts.Repositories;
using Contracts.Services;
using LanguageCourses.Application.Services;
using LanguageCourses.Persistence;
using LanguageCourses.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Web.Extensions;

public static class ServiceExtensions
{
	public static void ConfigureCors(this IServiceCollection services) =>
		services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy", builder =>
			builder.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());
		});

	public static void ConfigureSqlContext(this IServiceCollection services,
		IConfiguration configuration) =>
		services.AddDbContext<LanguageCoursesContext>(opts =>
			opts.UseSqlServer(configuration.GetConnectionString("localMSSQLServerConnection"), b =>
			{
				b.EnableRetryOnFailure();
			})
		);

	public static void ConfigureRepositoryManager(this IServiceCollection services) =>
		services.AddScoped<IRepositoryManager, RepositoryManager>();
	
	public static void ConfigureServiceManager(this IServiceCollection services) =>
		services.AddScoped<IServiceManager, ServiceManager>();
}
