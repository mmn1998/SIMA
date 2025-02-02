using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.CobitScenarios;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.CobitScenarios.Mappers;

public class CobitScenarioMapper : Profile
{
    public CobitScenarioMapper()
    {
        CreateMap<CreateCobitScenarioCommand, CreateCobitScenarioArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyCobitScenarioCommand, ModifyCobitScenarioArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}