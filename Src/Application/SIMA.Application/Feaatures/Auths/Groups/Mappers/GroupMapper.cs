
using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Groups;
using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.Groups.Mappers;

public class GroupMapper : Profile
{
    public GroupMapper(ISimaIdentity simaIdentity)
    {

        CreateMap<CreateGroupCommand, CreateGroupArg>()
    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
    .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (long)ActiveStatusEnum.Active))
    //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
    .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));
        ;


        CreateMap<UpdateGroupCommand, ModifyGroupArg>()
            .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
            //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
        CreateMap<CreateGroupPermissionCommand, CreateGroupPermissionArg>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
                .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (long)ActiveStatusEnum.Active));
        CreateMap<UpdateGroupPermissionCommand, ModifyGroupPermissionArg>()
            .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
            //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
        CreateMap<CreateGroupUserCommand, CreateUserGroupArg>()
            //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (long)ActiveStatusEnum.Active));
        CreateMap<UpdateGroupUserCommand, ModifyUserGroupArg>()
            .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
            //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
    }
}
