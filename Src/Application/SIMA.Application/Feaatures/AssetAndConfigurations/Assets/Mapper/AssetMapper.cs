using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.Assets.Mapper;

public class AssetMapper : Profile
{
    public AssetMapper()
    {
        CreateMap<CreateAssetCommand, CreateAssetArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ExpireDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ExpireDate)))
            ;
        CreateMap<ModifyAssetCommand, ModifyAssetArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
