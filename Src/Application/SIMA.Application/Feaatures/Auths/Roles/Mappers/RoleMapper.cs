using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Roles;
using SIMA.Domain.Models.Features.Auths.Roles.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.Roles.Mappers;

public class RoleMapper : Profile
{
    public RoleMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateRoleCommand, CreateRoleArg>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));
        CreateMap<CreateRolePermissionCommand, CreateRolePermissionArg>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
        CreateMap<UpdateRolePermissionCommand, ModifyRolePermissionArg>()
            .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
            .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
        CreateMap<UpdateRoleCommand, ModifyRoleArg>()
            .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
            .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
    }
}
