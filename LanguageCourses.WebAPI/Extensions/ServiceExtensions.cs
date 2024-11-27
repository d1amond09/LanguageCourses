using Contracts;
using Inno_Shop.Services.ProductAPI.Core.Application.Service;
using LanguageCourses.Application;
using LanguageCourses.Persistence;
using LanguageCourses.WebAPI.Formatters.Output;
using LoggerService;
using MediatR;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AspNetCoreRateLimit;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.WebAPI.Utility;
using LanguageCourses.WebAPI.ActionFilters;
using LanguageCourses.Application.Behaviors;
using Marvin.Cache.Headers;
using FluentValidation;
using LanguageCourses.WebAPI.GlobalException;
using Contracts.ModelLinks;
using LanguageCourses.Domain.ConfigurationModels;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LanguageCourses.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
        });

    public static void ConfigureExceptionHandler(this IServiceCollection services) =>
        services.AddExceptionHandler<GlobalExceptionHandler>();

    public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddDbContext<LanguageCoursesContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("ASPMonster_MSSQLServerConnection"), b =>
            {
                b.MigrationsAssembly("LanguageCourses.Persistence");
                b.EnableRetryOnFailure();
            })
        );

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureAutoMapper(this IServiceCollection services) =>
        services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

    public static void ConfigureMediatR(this IServiceCollection services) =>
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

    public static void ConfigureFluentValidation(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
    }

    public static void ConfigureResponseCaching(this IServiceCollection services) =>
        services.AddResponseCaching();

    public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
        services.AddHttpCacheHeaders(
            (expirationOpt) => {
                expirationOpt.MaxAge = 120;
                expirationOpt.CacheLocation = CacheLocation.Private;
            },
            (validationOpt) => {
                validationOpt.MustRevalidate = true;
            }
        );

    public static void AddCustomMediaTypes(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(config =>
        {
            var newtonsoftJsonOutputFormatter = config.OutputFormatters
                .OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

            newtonsoftJsonOutputFormatter?.SupportedMediaTypes
                .Add("application/hateoas+json");

            newtonsoftJsonOutputFormatter?.SupportedMediaTypes
                .Add("application/apiroot+json");
        });
    }

    public static void ConfigureRateLimitingOptions(this IServiceCollection services)
    {
        List<RateLimitRule> rateLimitRules = [
            new() {
                Endpoint = "*",
                Limit = 10,
                Period = "1s"
            }
        ];

        services.Configure<IpRateLimitOptions>(opt => {
            opt.GeneralRules = rateLimitRules;
        });

        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    }

    public static void ConfigureDataShaping(this IServiceCollection services)
    {
        services.AddScoped<IDataShaper<CourseDto>, DataShaper<CourseDto>>();
        services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
        services.AddScoped<IDataShaper<JobTitleDto>, DataShaper<JobTitleDto>>();
        services.AddScoped<IDataShaper<PaymentDto>, DataShaper<PaymentDto>>();
        services.AddScoped<IDataShaper<StudentDto>, DataShaper<StudentDto>>();
    }

    public static void ConfigureHATEOAS(this IServiceCollection services)
    {
        services.AddScoped<ICourseLinks, CourseLinks>();
        services.AddScoped<IStudentLinks, StudentLinks>();
        services.AddScoped<IJobTitleLinks, JobTitleLinks>();
        services.AddScoped<IPaymentLinks, PaymentLinks>();
        services.AddScoped<IEmployeeLinks, EmployeeLinks>();
        services.AddScoped<ValidateMediaTypeAttribute>();
    }

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
        builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

    public static void AddJwtAuthenticationConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtConfiguration>("JwtSettings", config.GetSection("JwtSettings"));
        var jwtConfiguration = new JwtConfiguration();
        config.Bind(jwtConfiguration.Section, jwtConfiguration);
        var secretKey = config.GetValue<string>("SECRET");
        ArgumentNullException.ThrowIfNull(secretKey);

        services.AddAuthentication("Bearer").AddJwtBearer("Bearer", opt =>
        {
            opt.Authority = jwtConfiguration.ValidAudience;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtConfiguration.ValidIssuer,
                ValidAudience = jwtConfiguration.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "LanguageCourses API",
                Version = "v1"
            });
            s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            s.EnableAnnotations();
            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            s.AddSecurityRequirement(new OpenApiSecurityRequirement() { {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Name = "Bearer",
                },
                new List<string>()
            } });
        });
    }
}
