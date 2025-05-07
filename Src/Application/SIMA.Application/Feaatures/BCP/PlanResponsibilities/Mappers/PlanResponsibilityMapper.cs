using AutoMapper;
using SIMA.Application.Contract.Features.BCP.PlanResponsibilities;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.PlanResponsibilities.Mappers;

public class PlanResponsibilityMapper : Profile
{
    public PlanResponsibilityMapper()
    {
        CreateMap<CreatePlanResponsibilityCommand, CreatePlanResponsibilityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyPlanResponsibilityCommand, ModifyPlanResponsibilityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}