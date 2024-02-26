
using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Permission;
using SIMA.Domain.Models.Features.Auths.Permissions.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;

namespace SIMA.Application.Feaatures.Auths.Permision.Mappers;

public class PermisionMapper : Profile
{
    public PermisionMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreatePermissionCommand, CreatePermissionArg>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));
        ;
    }
}
