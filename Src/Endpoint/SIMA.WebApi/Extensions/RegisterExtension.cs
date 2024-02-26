using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.IdentityModel.Tokens;
using SIMA.Application.Feaatures.Auths.Companies.Mappers;
using SIMA.Framework.Common.Security;
using SIMA.WebApi.Controllers.Features.Auths.Users.V1;
using SIMA.WebApi.Conventions;
using System.Reflection;
using System.Text;

namespace SIMA.WebApi.Extensions
{
    public static class RegisterExtension
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(c =>
            {
                c.Lifetime = ServiceLifetime.Scoped;
                c.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

            return services;
        }
        public static IServiceCollection RegisterConventions(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Conventions.Add(new ModelConvention());

            }).PartManager.ApplicationParts.Add(new AssemblyPart(typeof(UsersController).Assembly));
            return services;
        }
        public static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps(typeof(CompanyMapper).Assembly);
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
        public static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationHandler, PermissionsAuthorizationHandler>();

            TokenModel token = new();
            configuration.GetSection(nameof(TokenModel))
                .Bind(token);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = token.Issuer,
                    ValidIssuer = token.Issuer,

                    ClockSkew = TimeSpan.FromMinutes(5),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.SigningKey))
                };
            });

            services.AddAuthorization(options =>
            {
                var allPermissions = Enum.GetValues(typeof(Permissions)).Cast<Permissions>();

                foreach (var permission in allPermissions)
                {
                    options.AddPolicy($"{permission}", policy =>
                    {
                        policy.Requirements.Add(new PermissionRequirement(permission));
                    });
                }
            });
            return services;
        }
        public static IServiceCollection RegisterSimaIdentity(this IServiceCollection services)
        {
            return services.AddScoped<ISimaIdentity, SimaIdentity>();
        }
    }
}
