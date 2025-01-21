using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetTechnicalStatuses;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.AssetTechnicalStatuses.Mappers;

public class AssetTechnicalStatusMapper : Profile
{
    public AssetTechnicalStatusMapper()
    {
        CreateMap<CreateAssetTechnicalStatusCommand, CreateAssetTechnicalStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyAssetTechnicalStatusCommand, ModifyAssetTechnicalStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}