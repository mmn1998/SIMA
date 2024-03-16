using Serilog;
using Serilog.Sinks.Elasticsearch;
using SIMA.Application.ConfigurationExtensions;
using SIMA.Application.Query.ConfigurationExtensions;
using SIMA.DomainService.ConfigurationExtensions;
using SIMA.Framework.Common.Cachings;
using SIMA.Framework.Common.Helper.FileHelper;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Framework.WebApi;
using SIMA.Framework.WebApi.ConfigurationExtention;
using SIMA.Framework.WebApi.Services;
using SIMA.Persistance.ConfigurationExtentions;
using SIMA.Persistance.Read.ConfigurationExtensions;
using SIMA.WebApi.Extensions;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using SIMA.WebApi.Middlwares;
using System.Globalization;
using SIMA.WebApi.Dtos;

#region AddConfigurationFiles
var configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .AddJsonFile("appsettings.Development.json")
              .AddJsonFile("appsettings.RedisConfigs.json")
              .Build();
#endregion

#region Serilog Settings
var logSettings = configuration.GetSection("LogSettings").Get<LogSettings>() ?? new();
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithClientIp()
    //.Enrich.WithClientAgent()
    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder().WithDefaultDestructurers())
    .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
    .WriteTo.File(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error, path: logSettings.ErrorFilePath, rollingInterval: RollingInterval.Day)

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
    Log.Error("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    const string AllowEveryThingPolicy = "AllowEveryThing";
    builder.Services.AddControllers();

    builder.Services.Configure<TokenModel>(
        builder.Configuration.GetSection(
            key: nameof(TokenModel)));
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

    string CipherConnectionString = builder.Configuration.GetConnectionString("UserManagementCipher") ?? "";
    string SignedConnectionString = builder.Configuration.GetConnectionString("UserManagementSign") ?? "";
    string connectionString = builder.Configuration.GetDecriptedValue(CipherConnectionString, SignedConnectionString);

    builder.Services.AddMemoryCache();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSingleton<IFileService, FileService>();
    builder.Services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();
    builder.Services.AddSingleton<ICaptchaService, CaptchaService>();
    builder.Services.RegisterAuthentication(builder.Configuration)
                    .AddCommandHandlerServices()
                    .RegisterWriteDbContext(connectionString)
                    .RegisterApplicationServices()
                    .RegisterQueryRepositories()
                    .RegisterCommandRepository()
                    .RegisterUnitOfWork()
                    .AddEndpointsApiExplorer()
                    .RegisterQueryHandlerService()
                    .AddSimaSwagger()
                    .RegisterDomainServices()
                    .RegisterQueryMappers()
                    .RegisterCommandMappers()
                    .RegisterConventions()
                    .RegisterSimaIdentity()
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
    app.UseDisAllowDirectDownloadMiddleware();
    //app.UseStaticFiles();
    app.UseCors(AllowEveryThingPolicy);
    app.UseMiddleware<ExceptionMiddleware>();
    //app.UseMiddleware<SanitizationMiddleware>();
    //app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseRequestResponseLogging();
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
