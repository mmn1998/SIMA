using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.CriticalActivities;
using SIMA.Application.Contract.Features.ServiceCatalog.Services;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.CriticalActivities.Mappers;

public class CriticalActivityMapper : Profile
{
    public CriticalActivityMapper()
    {
        CreateMap<CreateCriticalActivityCommand, CreateCriticalActivityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.IssueId, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyCriticalActivityCommand, ModifyCriticalActivityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<long, CreateCriticalActivityServicesArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ServiceId, act => act.MapFrom(source => source))
            ;
        CreateMap<long, CreateCriticalActivityRiskArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.RiskId, act => act.MapFrom(source => source))
            ;
        CreateMap<long, CreateCriticalActivityAssetArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.AssetId, act => act.MapFrom(source => source))
            ;
        CreateMap<long, CreateCriticalActivityConfigurationItemArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ConfigurationItemId, act => act.MapFrom(source => source))
            ;
        CreateMap<CreateServiceAssignedStaffCommand, CreateCriticalActivityAssignedStaffArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<CreateCriticalActivityExecutionPlanCommand, CreateCriticalActivityExecutionPlanArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ServiceAvalibilityStartTime, act => act.MapFrom(source => source.ServiceAvalibilityStartTime.ToTimeOnly()))
            .ForMember(dest => dest.ServiceAvalibilityEndTime, act => act.MapFrom(source => source.ServiceAvalibilityEndTime.ToTimeOnly()))
            ;
    }
}
