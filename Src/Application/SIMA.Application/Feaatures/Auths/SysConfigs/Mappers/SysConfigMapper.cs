using AutoMapper;
using SIMA.Application.Contract.Features.Auths.SysConfigs;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;

namespace SIMA.Application.Feaatures.Auths.SysConfigs.Mappers;

internal class SysConfigMapper : Profile
{
    public SysConfigMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateSystemConfigurationCommand, CreateSysConfigArg>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));
    }
}
