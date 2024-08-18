using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Sinks.Elasticsearch;
using SIMA.Application.ConfigurationExtensions;
using SIMA.Application.Query.ConfigurationExtensions;
using SIMA.DomainService.ConfigurationExtensions;
using SIMA.Framework.Common.Cachings;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper.FileHelper;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Common.Services;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Framework.WebApi;
using SIMA.Framework.WebApi.ConfigurationExtention;
using SIMA.Framework.WebApi.Services;
using SIMA.Persistance.ConfigurationExtentions;
using SIMA.Persistance.Read.ConfigurationExtensions;
using SIMA.WebApi.Dtos;
using SIMA.WebApi.Extensions;
using SIMA.WebApi.Middlwares;
using SIMA.WebApi.Settings;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

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
    .Enrich.WithClientIp()
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
                    .AddSimaSwagger(appVersion)
                    .RegisterDomainServices()
                    .RegisterQueryMappers()
                    .RegisterCommandMappers()
                    .RegisterConventions()
                    .RegisterSimaIdentity()
                    .RegisterTokenService()
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

public class PermissionsAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IDistributedCache _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    private readonly TokenModel _securitySettings;

    public PermissionsAuthorizationHandler(IDistributedCache cache, IHttpContextAccessor httpContextAccessor,
        IOptions<TokenModel> securitySettings, IConfiguration configuration, ITokenService tokenService)
    {
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _tokenService = tokenService;
        _securitySettings = securitySettings.Value;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        //if (await IsRefreshTokenAlive())
        //{
        if ((!context.User.Identity?.IsAuthenticated) ?? false)
        {
            return;
        }
        var permissionString = context.User.Claims.FirstOrDefault(c => c.Type == "Permissions");

        var hasPermissionClaim = PermissionChecker.ThisPermissionIsAllowed(permissionString.Value, requirement.Permission.ToString());
        if (hasPermissionClaim)
        {
            context.Succeed(requirement);
        }
        //}

        return;
    }
    private async Task<bool> IsRefreshTokenAlive()
    {
        bool result = false;
        var redisInstanceName = _configuration.GetSection("RedisSettings").GetValue<string>("InstanceName");
        var token = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var expiredPrincipal = _tokenService.GetPrincipalFromExpiredToken(token);
                if (expiredPrincipal != null)
                {
                    var userName = expiredPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                    if (userName != null)
                    {
                        string refreshTokenHeaderName = "Sigma";
                        var realRefreshToken = await _cache.GetStringAsync($"{redisInstanceName}{userName}");
                        var sendedRefreshToken = _httpContextAccessor.HttpContext?.Request.Headers[refreshTokenHeaderName].ToString();
                        if (realRefreshToken != null && string.Equals(realRefreshToken, sendedRefreshToken, StringComparison.InvariantCultureIgnoreCase))
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw SimaResultException.UnAuthorize;
            }
        }
        return result;
    }
    private ClaimsPrincipal GetClaimsFromExpiredToken(string token)
    {
        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securitySettings.SigningKey)),
                ValidateLifetime = false // This will not validate the token's expiration
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }
        catch (Exception)
        {
            throw SimaResultException.UnAuthorize;
        }
    }
}
