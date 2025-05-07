using AutoMapper;
using SIMA.Application.Contract.Features.Auths.TimeMeasurements;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Arg;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.TimeMeasurements.Mappers;

public class TimeMeasurementMapper : Profile
{
    public TimeMeasurementMapper()
    {
        CreateMap<CreateTimeMeasurementCommand, CreateTimeMeasurementArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyTimeMeasurementCommand, ModifyTimeMeasurementArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}