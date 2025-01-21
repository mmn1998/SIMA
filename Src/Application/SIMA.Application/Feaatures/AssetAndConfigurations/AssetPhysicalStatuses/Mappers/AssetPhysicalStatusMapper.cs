using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetPhysicalStatuses;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.AssetPhysicalStatuses.Mappers;

public class AssetPhysicalStatusMapper : Profile
{
    public AssetPhysicalStatusMapper()
    {
        CreateMap<CreateAssetPhysicalStatusCommand, CreateAssetPhysicalStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyAssetPhysicalStatusCommand, ModifyAssetPhysicalStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}