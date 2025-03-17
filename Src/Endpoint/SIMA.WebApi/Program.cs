using AspNetCoreRateLimit;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Sinks.Elasticsearch;
using SIMA.Application.ConfigurationExtensions;
using SIMA.Application.Query.ConfigurationExtensions;
using SIMA.Application.Query.Services.ReportServices;
using SIMA.Application.Services;
using SIMA.Application.Services.BehsazanServices;
using SIMA.DomainService.ConfigurationExtensions;
using SIMA.Framework.Common.Cachings;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper.FileHelper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Common.Services;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Framework.Infrastructure.RestfulClient;
using SIMA.Framework.WebApi;
using SIMA.Framework.WebApi.ConfigurationExtention;
using SIMA.Framework.WebApi.Services;
using SIMA.Persistance.ConfigurationExtentions;
using SIMA.Persistance.Read.ConfigurationExtensions;
using SIMA.WebApi.Dtos;
using SIMA.WebApi.Extensions;
using SIMA.WebApi.Middlwares;
using SIMA.WebApi.Settings;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

#region AddConfigurationFiles
var configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .AddJsonFile("appsettings.Development.json")
              .AddJsonFile("appsettings.RedisConfigs.json")
              .AddJsonFile("appsettings.RateLimit.json")
              .Build();
#endregion

#region Serilog Settings
var logSettings = configuration.GetSection("LogSettings").Get<LogSettings>() ?? new();
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    //.Enrich.WithClientIp()
    //.Enrich.WithClientAgent()
    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder().WithDefaultDestructurers())
    .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
    .WriteTo.File(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, path: logSettings.ErrorFilePath, rollingInterval: RollingInterval.Day)

    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(logSettings.ElasticsearchUri))
    {
        AutoRegisterTemplate = true,
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
        IndexFormat = "Sima" + "-{0:yyyy.MM}",
        RegisterTemplateFailure = RegisterTemplateRecovery.IndexAnyway,
        EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog | EmitEventFailureHandling.RaiseCallback | EmitEventFailureHandling.ThrowException,
        FailureCallback = e => { Console.WriteLine("Elasticsearch Error: " + e.MessageTemplate); },
        MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information
    })
    .CreateLogger();
#endregion
try
{

    var appVersion = typeof(Program).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+')[0];
    Log.Error("Starting web application");

    var builder = WebApplication.CreateBuilder(args);
    // Configure Kestrel server options
    //builder.WebHost.ConfigureKestrel(options =>
    //{
    //    options.Limits.MaxRequestBodySize = 4194304; // 4 MB (adjust the value as needed)
    //});
    // Add services to the container.
    const string AllowEveryThingPolicy = "AllowEveryThing";
    builder.Services.AddControllers();

    builder.Services.Configure<TokenModel>(
        builder.Configuration.GetSection(
            key: nameof(TokenModel)));
    builder.Services.Configure<PasswordPolicy>(
        builder.Configuration.GetSection(
            key: nameof(PasswordPolicy)));
    builder.Services.Configure<ApplicationSettings>(
        builder.Configuration.GetSection(
            key: nameof(ApplicationSettings)));
    #region RateLimit(DDOS prevention)
    builder.Services.AddMemoryCache();
    builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
    builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

    builder.Services.AddInMemoryRateLimiting();

    builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

    #endregion
    #region Redis

    var redisSettingSection = configuration.GetSection("RedisSettings");
    string cipherOfRedisConnectionString = redisSettingSection.GetValue<string>("CipherConnectionString") ?? "";
    string signOfRedisConnectionString = redisSettingSection.GetValue<string>("SignedConnectionString") ?? "";
    string plainRedisConnectionString = builder.Configuration.GetDecriptedValue(cipherOfRedisConnectionString, signOfRedisConnectionString);
    var redisSettings = configuration.GetSection("RedisSettings").Get<RedisSettings>() ?? new();
    redisSettings.ConnectionString = plainRedisConnectionString;
    builder.Services.AddSimaRedis(redisSettings);

    #endregion
    #region Registrations

    //builder.Services.Configure<FormOptions>(options =>
    //{
    //    options.MultipartBodyLengthLimit = 4194304; // 4 MB (adjust the value as needed)
    //});

    string CipherConnectionString = builder.Configuration.GetConnectionString("UserManagementCipher") ?? "";
    string SignedConnectionString = builder.Configuration.GetConnectionString("UserManagementSign") ?? "";
    string connectionString = builder.Configuration.GetDecriptedValue(CipherConnectionString, SignedConnectionString);
    //string connectionString = "Server=172.20.156.178,49235;Database=SIMADBBehsazanNew;User Id=DV_User;Password=2YR@&jdppAya;TrustServerCertificate=True;";
    //string connectionString = "Server=185.105.239.136;Database=SIMADBBank;User Id=foad;Password=foad1qaz!QAZ;TrustServerCertificate=True;";

    builder.Services.AddMemoryCache();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSingleton<IFileService, FileService>();
    builder.Services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();
    builder.Services.AddSingleton<ICaptchaService, CaptchaService>();
    builder.Services.Configure<SMSSetting>(builder.Configuration.GetSection(nameof(SMSSetting)));
    builder.Services.Configure<BehsazanServiceSetting>(builder.Configuration.GetSection(nameof(BehsazanServiceSetting)));
    builder.Services.RegisterAuthentication(builder.Configuration)
                    .AddCommandHandlerServices()
                    .RegisterWriteDbContext(connectionString)
                    .RegisterApplicationServices()
                    .RegisterQueryRepositories()
                    .RegisterCommandRepository()
                    .RegisterUnitOfWork()
                    .AddEndpointsApiExplorer()
                    .RegisterQueryHandlerService()
                    .AddSimaSwagger(appVersion)
                    .RegisterDomainServices()
                    .RegisterQueryMappers()
                    .RegisterCommandMappers()
                    .RegisterConventions()
                    .RegisterSimaIdentity()
                    .RegisterTokenService()
                    .AddTransient<IRestfulClient, RestfulClient>()
                    .AddTransient<ISMSService, SMSService>()
                    .AddTransient<IBehsazanService, BehsazanService>()
                    .AddTransient<IBankReportService, BankReportService>()
                    ;
    #endregion
    #region Cors
    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy(AllowEveryThingPolicy, policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });
    #endregion

    builder.Host.UseSerilog();

    var app = builder.Build();

    CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;


    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseMiddleware<LicenseCheckerMiddleware>();
    app.UseDisAllowDirectDownloadMiddleware();
    //app.UseStaticFiles();
    app.UseCors(AllowEveryThingPolicy);
    app.UseMiddleware<ExceptionMiddleware>();
    //app.UseMiddleware<SanitizationMiddleware>();
    //app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseRequestResponseLogging();
    app.UseClientIpEnrichmentMiddleware();
    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");

}
finally
{
    Log.CloseAndFlush();
}
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        Stream body = httpContext.Response.Body;
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            httpContext.Response.Body = body;
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    public virtual int ExceptionStatusCodeMapping(Exception ex)
    {
        if (1 == 0)
        {
        }

        int result = ((ex is InvalidCastException) ? 400 : ((ex is InvalidOperationException) ? 201 : ((ex is ValidationException) ? 401 : ((!(ex is SimaException)) ? 500 : 200))));
        if (1 == 0)
        {
        }

        return result;
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int status = ExceptionStatusCodeMapping(exception);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = status;
        await HttpResponseWritingExtensions.WriteAsync(text: JsonSerializer.Serialize(GetException(exception), new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }), response: context.Response);
        if (status >= 500)
        {
            _logger.LogError(exception, "خطای سرور رخ داده است");
        }
        else
        {
            _logger.LogInformation(exception, "Handled exception occurred");
        }
    }

    private Result GetException(Exception exception)
    {
        if (exception is SimaException ex)
        {
            return ex.Error;
        }

        return SimaResultException.ServerError;
    }
}
