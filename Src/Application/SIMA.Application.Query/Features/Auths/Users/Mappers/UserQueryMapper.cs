using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Framework.Common.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SIMA.Application.Query.Features.Auths.Users.Mappers;

public class UserQueryMapper : Profile
{
    //private static Contract.Security.TokenModel? _securitySettings;

    public UserQueryMapper()
    {
        //_securitySettings = securitySettings.Value;
        //_simaIdentity = simaIdentity;
        CreateMap<UserLocationAccess, GetUserLocationQueryResult>();
        CreateMap<UserPermission, GetUserPermissionQueryResult>();
        CreateMap<UserDomainAccess, GetUserDomainQueryResult>();
        CreateMap<UserRole, GetUserRoleQueryResult>();
    }

    public static LoginUserQueryResult MapToToken(LoginUserQueryResult user, TokenModel _securitySettings)
    {
        var claims = GenereteClaim(user);
        user.Token = GenerateToken(claims, _securitySettings);
        return user;
    }
    private static string GenerateToken(IEnumerable<Claim> claims, TokenModel tokenModel)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenModel.SigningKey));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = tokenModel.Issuer,
            Audience = tokenModel.Issuer,
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    private static IEnumerable<Claim> GenereteClaim(LoginUserQueryResult user)
    {
        //todo Mahmood
        var permissions = user.Permissions.PackPermissionsIntoString();
        //var permissions = "";
        return new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.UserInfoLogin.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserInfoLogin.Username),
            new Claim("CompanyId", user.UserInfoLogin.CompanyId.ToString()),
            new Claim(ClaimTypes.Role, JsonSerializer.Serialize(user.RoleIds)),
            new Claim("Groups", JsonSerializer.Serialize(user.GroupIds)),
            new Claim("Permissions", permissions)
        };
    }
    public static bool UserHasThisPermission(IEnumerable<Framework.Common.Security.Permissions> usersPermissions, Framework.Common.Security.Permissions permissionToCheck)
    {
        return usersPermissions.Contains(permissionToCheck);
    }
    //public static ClaimsPrincipal GetPrincipalFromToken(string token)
    //{
    //    var tokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
    //        ValidateIssuer = false,
    //        ValidateIssuerSigningKey = true,
    //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securitySettings.SigningKey)),
    //        ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
    //    };
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    SecurityToken securityToken;
    //    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
    //    var jwtSecurityToken = securityToken as JwtSecurityToken;
    //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
    //        throw new SecurityTokenException("Invalid token");
    //    return principal;
    //}
    //public int GetUserId(ClaimsPrincipal principal)
    //{
    //    return Convert.ToInt32(principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
    //}
    //public string GetUsername(ClaimsPrincipal principal)
    //{
    //    return principal.Claims.First(c => c.Type == ClaimTypes.Name).Value;
    //}
}
