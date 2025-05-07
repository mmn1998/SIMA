using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemStatuses;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.ConfigurationItemStatuses.Mappers;

public class ConfigurationItemStatusMapper : Profile
{
    public ConfigurationItemStatusMapper()
    {
        CreateMap<CreateConfigurationItemStatusCommand, CreateConfigurationItemStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyConfigurationItemStatusCommand, ModifyConfigurationItemStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}