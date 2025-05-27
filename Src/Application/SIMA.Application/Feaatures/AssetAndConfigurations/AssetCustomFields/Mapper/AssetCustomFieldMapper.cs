using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetCustomFields;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.AssetCustomFields.Mapper
{
    public class AssetCustomFieldMapper : Profile
    {
        public AssetCustomFieldMapper()
        {
            CreateMap<CreateAssetCustomFieldCommand, CreateAssetCustomFieldArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
           ;


            CreateMap<CreateAssetCustomFieldOption, CreateAssetCustomFieldOptionArg>()
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
          ;

            CreateMap<ModifyAssetCustomFieldCommand, ModifyAssetCustomFieldArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
                ;
        }
    }
}
