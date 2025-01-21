using AutoMapper;
using SIMA.Application.Contract.Features.BCP.StrategyTypes;
using SIMA.Domain.Models.Features.BCP.StrategyTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.StrategyTypes.Mappers;

public class StrategyTypeMapper : Profile
{
    public StrategyTypeMapper()
    {
        CreateMap<CreateStrategyTypeCommand, CreateStrategyTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyStrategyTypeCommand, ModifyStrategyTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}