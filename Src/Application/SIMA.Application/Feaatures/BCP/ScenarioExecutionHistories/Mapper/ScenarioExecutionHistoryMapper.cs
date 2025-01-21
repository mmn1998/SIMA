using AutoMapper;
using SIMA.Application.Contract.Features.BCP.ScenarioExecutionHistories;
using SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.ScenarioExecutionHistories.Mapper;

public class ScenarioExecutionHistoryMapper : Profile
{
    public ScenarioExecutionHistoryMapper()
    {
        CreateMap<CreateScenarioExecutionHistoryCommand, CreateScenarioExecutionHistoryArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       .ForMember(dest => dest.ExecutionDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ExecutionDate)))
       .ForMember(dest => dest.ExecutionTimeTo, act => act.MapFrom(source => DateHelper.ToTimeOnly(source.ExecutionTimeTo)))
       .ForMember(dest => dest.ExecutionTimeFrom, act => act.MapFrom(source => DateHelper.ToTimeOnly(source.ExecutionTimeFrom)))
       ;
        CreateMap<ModifyScenarioExecutionHistoryCommand, ModifyScenarioExecutionHistoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            .ForMember(dest => dest.ExecutionDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ExecutionDate)))
            .ForMember(dest => dest.ExecutionTimeTo, act => act.MapFrom(source => DateHelper.ToTimeOnly(source.ExecutionTimeTo)))
            .ForMember(dest => dest.ExecutionTimeFrom, act => act.MapFrom(source => DateHelper.ToTimeOnly(source.ExecutionTimeFrom)))
            ;
    }
}
