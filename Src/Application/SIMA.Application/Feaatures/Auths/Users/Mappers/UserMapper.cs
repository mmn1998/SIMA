using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SIMA.Application.Contract.Features.Auths.Users;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Common.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
namespace SIMA.Application.Feaatures.Auths.Users.Mappers;

public class UserMapper : Profile
{
    public UserMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateUserCommand, CreateUserArg>()
            .ForMember(x => x.ActiveFrom, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.Now)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
            .ForMember(x => x.IsFirstLogin, opt => opt.MapFrom(src => "1"))
            .ForMember(x => x.IsLocked, opt => opt.MapFrom(src => "0"))
            .ForMember(x => x.IsSendOTP, opt => opt.MapFrom(src => "1"))
            ////.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (long)ActiveStatusEnum.Active));


        CreateMap<CreateUserAggregateCommand, CreateUserArg>()
            .ForMember(x => x.ActiveFrom, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.Now)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
            .ForMember(x => x.IsFirstLogin, opt => opt.MapFrom(src => "1"))
            .ForMember(x => x.IsLocked, opt => opt.MapFrom(src => "0"))
            .ForMember(x => x.IsSendOTP, opt => opt.MapFrom(src => "1"))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (long)ActiveStatusEnum.Active));


        CreateMap<CreateUserRoleCommand, CreateUserRoleArg>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (long)ActiveStatusEnum.Active));
        CreateMap<CreateUserPermissionCommand, CreateUserPermissionArg>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (long)ActiveStatusEnum.Active));
        CreateMap<CreateFormUserCommand, CreateFormUserArg>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (long)ActiveStatusEnum.Active));
        CreateMap<CreateUserLocationCommand, CreateUserLocationAccessArg>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (long)ActiveStatusEnum.Active));
        CreateMap<UpdateUserCommand, ModifyUserArg>()
            .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
        //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId));
        ;


        CreateMap<UpdateUserLocationCommand, ModifyUserLocationArg>()
            .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
            //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
        CreateMap<UpdateUserPermissionCommand, ModifyUserPermissionArg>()
            .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
            //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
        CreateMap<UpdateUserRoleCommand, ModifyUserRoleArg>()
            .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
            //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;

        CreateMap<ChangePasswordCommand, ChangePasswordArg>()
            .ForMember(dest => dest.ChangePasswordDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => "0"))
            .ForMember(x => x.IsFirstLogin, opt => opt.MapFrom(src => "0"));

        CreateMap<CreateUserGroupCommand, CreateUserGroupArg>()
      .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
      .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (long)ActiveStatusEnum.Active));


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
