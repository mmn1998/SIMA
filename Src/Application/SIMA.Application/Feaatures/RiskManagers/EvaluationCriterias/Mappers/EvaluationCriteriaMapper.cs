using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.EvaluationCriterias;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.EvaluationCriterias.Mappers;

public class EvaluationCriteriaMapper : Profile
{
    public EvaluationCriteriaMapper()
    {
        CreateMap<CreateEvaluationCriteriaCommand, CreateEvaluationCriteriaArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyEvaluationCriteriaCommand, ModifyEvaluationCriteriaArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}