using System.Text;
using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.EvaluationCriterias;
using SIMA.Application.Contract.Features.RiskManagers.ScenarioHistories;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Args;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Args;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Feaatures.RiskManagers.ScenarioHistories.Mapper;

public class ScenarioHistoryMapper : Profile
{
    public ScenarioHistoryMapper()
    {
        CreateMap<CreateScenarioHistoryCommand, CreateScenarioHistoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyScenarioHistoryCommand, ModifyScenarioHistoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}