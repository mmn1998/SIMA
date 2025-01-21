using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetTypes;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.AssetTypes.Mappers;

public class AssetTypeMapper : Profile
{
    public AssetTypeMapper()
    {
        CreateMap<CreateAssetTypeCommand, CreateAssetTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyAssetTypeCommand, ModifyAssetTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}