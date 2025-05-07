using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemCustomFields;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.ConfigurationItemCustomFields.Mapper
{
    public class ConfigurationItemCustomFieldMapper : Profile
    {
        public ConfigurationItemCustomFieldMapper()
        {
            CreateMap<CreateConfigurationItemCustomFieldCommand, CreateConfigurationItemCustomFieldArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
           ;


            CreateMap<CreateAssetcustomfieldoption, CreateConfigurationItemCustomFieldOptionArg>()
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
          ;

            CreateMap<ModifyConfigurationItemCustomFieldCommand, ModifyConfigurationItemCustomFieldArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
                ;
        }
    }
}
