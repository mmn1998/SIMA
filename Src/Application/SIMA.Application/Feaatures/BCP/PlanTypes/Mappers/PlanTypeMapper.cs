using AutoMapper;
using SIMA.Application.Contract.Features.BCP.PlanTypes;
using SIMA.Domain.Models.Features.BCP.PlanTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.PlanTypes.Mappers;

public class PlanTypeMapper : Profile
{
    public PlanTypeMapper()
    {
        CreateMap<CreatePlanTypeCommand, CreatePlanTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyPlanTypeCommand, ModifyPlanTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}