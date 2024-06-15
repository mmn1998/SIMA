using AutoMapper;
using SIMA.Application.Contract.Features.Logistics.UnitMeasurements;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Logistics.UnitMeasurements.Mappers;

public class UnitMeasurementMapper : Profile
{
    public UnitMeasurementMapper()
    {
        CreateMap<CreateUnitMeasurementCommand, CreateUnitMeasurementArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyUnitMeasurementCommand, ModifyUnitMeasurementArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}