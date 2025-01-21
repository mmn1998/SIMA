using AutoMapper;
using SIMA.Application.Contract.Features.BCP.BusinessContinuityPlans;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Args;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Feaatures.BCP.BusinessContinuityPlans.Mapper;

public class BusinessContinuityPlanMapper : Profile
{
    public BusinessContinuityPlanMapper()
    {
        CreateMap<CreateBusinessContinuityPlanCommand, CreateBusinessContinuityPlanArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        .ForMember(dest => dest.IssueId, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        .ForMember(dest => dest.Code, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        ;


        CreateMap<CreateBusinessContinuityPlanCommand, CreateBusinessContinuityPlanVersioningArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.ReleaseDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ReleaseDate)))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))

       ;

        CreateMap<CreateBusinessContinuityPlanStratgyCommand, CreateBusinessContinuityPlanStratgyArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;

        CreateMap<CreateBusinessContinuityPlanServiceCommand, CreateBusinessContinuityPlanServiceArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       ;

        CreateMap<CreateBusinessContinuityPlanRiskCommand, CreateBusinessContinuityPlanRiskArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       ;

        CreateMap<CreateBusinessContinuityPlanCriticalActivityCommand, CreateBusinessContinuityPlanCriticalActivityArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       ;

        CreateMap<CreateBusinessContinuityPlanRelatedStaffCommand, CreateBusinessContinuityPlanRelatedStaffArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       ;

        CreateMap<CreateBusinessContinuityPlanResponsibleCommand, CreateBusinessContinuityPlanResponsibleArg>()
      .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
      .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
      ;

        CreateMap<CreateBusinessContinuityPlanAssumptionCommand, CreateBusinessContinuityPlanAssumptionArg>()
     .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
     .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
     ;
    }
}
