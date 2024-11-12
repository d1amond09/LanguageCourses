using AutoMapper;
using Contracts;
using LanguageCourses.Application;
using LanguageCourses.WebMVC.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace LanguageCourses.WebMVC
{
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
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            ConfigureApp(app);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddResponseCaching();
            services.Configure<ResponseCachingOptions>(options =>
            {
                options.MaximumBodySize = 1024 * 1024;
                options.UseCaseSensitivePaths = false;
            });
            services.AddControllersWithViews();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper autoMapper = mappingConfig.CreateMapper();
            services.AddSingleton(autoMapper);


            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();


            services.ConfigureSqlContext(configuration);

            services.ConfigureRepositoryManager();
            services.ConfigureServiceManager();
        }

        public static void ConfigureApp(IApplicationBuilder app)
        {
            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();   
        }
    }
}
