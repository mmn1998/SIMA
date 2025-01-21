using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Common.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SIMA.Application.Query.Features.Auths.Users.Mappers;

public class UserQueryMapper : Profile
{
    public UserQueryMapper()
    {
        CreateMap<UserLocationAccess, GetUserLocationQueryResult>();
        CreateMap<UserPermission, GetUserPermissionQueryResult>();
        CreateMap<UserRole, GetUserRoleQueryResult>();
    }

    public static LoginUserQueryResult MapToToken(LoginUserQueryResult user, ITokenService tokenService)
    {
        var claims = GenereteClaim(user);
        var res = GenerateToken(claims, tokenService);
        user.Token = res.AccessToken;
        user.RefreshToken = res.RefreshToken;
        return user;
    }
    private static TokenModelResult GenerateToken(IEnumerable<Claim> claims, ITokenService tokenService)
    {
        var accessToken = tokenService.GenerateAccessToken(claims);

        var refreshToken = tokenService.GenerateRefreshToken();

        return new TokenModelResult
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
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

}
