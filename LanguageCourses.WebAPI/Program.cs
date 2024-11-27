using AspNetCoreRateLimit;
using Contracts;
using LanguageCourses.WebAPI.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace LanguageCourses.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        LogManager.Setup().LoadConfigurationFromFile("nlog.config", true);

        ConfigureServices(builder.Services, builder.Configuration);
        var app = builder.Build();
        var logger = app.Services.GetRequiredService<ILoggerManager>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("./v1/swagger.json", "Product API v1");
            });
        }


        ConfigureApp(app);

        app.MapControllers();

        app.Run();
    }

    public static void ConfigureServices(IServiceCollection s, IConfiguration config)
    {
        s.AddProblemDetails();
        s.ConfigureLoggerService();
        s.ConfigureExceptionHandler();
        s.ConfigureCors();

        s.ConfigureSqlContext(config);
        s.ConfigureRepositoryManager();
        s.ConfigureMediatR();

        s.ConfigureAutoMapper();
        s.ConfigureFluentValidation();

        s.ConfigureResponseCaching();
        s.ConfigureHttpCacheHeaders();
        s.AddMemoryCache();

        s.AddEndpointsApiExplorer();
        s.ConfigureSwagger();

        s.AddJwtAuthenticationConfiguration(config);

        s.ConfigureDataShaping();
        s.ConfigureHATEOAS();

        s.ConfigureRateLimitingOptions();
        s.AddHttpContextAccessor();

        s.AddControllers(config =>
        {
            config.RespectBrowserAcceptHeader = true;
            config.ReturnHttpNotAcceptable = true;
            config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
            {
                Duration = 120
            });
        }).AddNewtonsoftJson();

        s.AddCustomMediaTypes();

        s.AddAuthorization();
    }

    public static void ConfigureApp(IApplicationBuilder app)
    {
        app.UseExceptionHandler();
        app.UseIpRateLimiting();
        app.UseCors("CorsPolicy");
        app.UseResponseCaching();
        app.UseHttpCacheHeaders();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
    }
}
