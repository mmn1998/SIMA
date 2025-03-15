using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.DataCenters;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.DataCenters.Mappers;

public class DataCenterMapper : Profile
{
    public DataCenterMapper()
    {
        CreateMap<CreateDataCenterCommand, CreateDataCenterArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyDataCenterCommand, ModifyDataCenterArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}