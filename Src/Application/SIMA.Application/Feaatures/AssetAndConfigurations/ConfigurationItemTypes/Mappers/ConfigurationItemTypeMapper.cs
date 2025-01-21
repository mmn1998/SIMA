using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemTypes;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.AssetTypes.Mappers;

public class ConfigurationItemTypeMapper : Profile
{
    public ConfigurationItemTypeMapper()
    {
        CreateMap<CreateConfigurationItemTypeCommand, CreateConfigurationItemTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyConfigurationItemTypeCommand, ModifyConfigurationItemTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}