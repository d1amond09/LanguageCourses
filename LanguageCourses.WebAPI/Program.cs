using Contracts;
using LanguageCourses.WebAPI.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
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
        app.ConfigureExceptionHandler(logger);
        if (app.Environment.IsProduction())
            app.UseHsts();


        ConfigureApp(app);

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureCors();
        services.ConfigureIISIntegration();
        services.ConfigureLoggerService();
        services.AddAutoMapper(typeof(Program));

        services.AddControllers(config =>
        {
            config.RespectBrowserAcceptHeader = true;
            config.ReturnHttpNotAcceptable = true;
        }).AddXmlDataContractSerializerFormatters()
         .AddCustomCSVFormatter();


        services.ConfigureSqlContext(configuration);

        services.ConfigureRepositoryManager();
        services.ConfigureServiceManager();
    }

    public static void ConfigureApp(IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });

        app.UseCors("CorsPolicy");
    }
}
