using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemRelationshipTypes;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.ConfigurationItemRelationshipTypes.Mappers;

public class ConfigurationItemRelationshipTypeMapper : Profile
{
    public ConfigurationItemRelationshipTypeMapper()
    {
        CreateMap<CreateConfigurationItemRelationshipTypeCommand, CreateConfigurationItemRelationshipTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyConfigurationItemRelationshipTypeCommand, ModifyConfigurationItemRelationshipTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}