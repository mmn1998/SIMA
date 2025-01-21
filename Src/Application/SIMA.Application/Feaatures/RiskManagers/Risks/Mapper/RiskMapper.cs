using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.Risks;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Args;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Args;
using SIMA.Domain.Models.Features.RiskManagement.Vulnerabilities.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.Risks.Mapper;

public class RiskMapper : Profile
{
    public RiskMapper()
    {
        CreateMap<CreateRiskCommand, CreateRiskArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        ;
        CreateMap<ModifyRiskCommand, ModifyRiskArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
        ;
        CreateMap<long, CreateRiskStaffArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.StaffId, act => act.MapFrom(source => source))
            ;
        CreateMap<string, CreateCorrectiveActionArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        .ForMember(dest => dest.ActionDescription, act => act.MapFrom(source => source))
        ;

        CreateMap<string, CreatePreventiveActionArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        .ForMember(dest => dest.ActionDescription, act => act.MapFrom(source => source))
        ;

        CreateMap<CreateEffectedAssetCommand, CreateEffectedAssetArgs>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        ;

        CreateMap<CreateEffectedAssetCommand, ModifyEffectedAssetArgs>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        .ForMember(dest => dest.VulnerabilityList, act => act.MapFrom(source => source.VulnerabilityList))

        ;

        CreateMap<CreateVulnerabilityCommand, CreateVulnerabilityArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        .ForMember(dest => dest.Description, act => act.MapFrom(source => source.Description))
        ;

        CreateMap<CreateserviceRiskImpactCommand, CreateServiceRiskArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        ;

        CreateMap<CreateserviceRiskImpactCommand, ModifyServiceRiskArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        .ForMember(dest => dest.ServiceRiskImpacts, act => act.MapFrom(source => source.RiskImpactList))
        ;

        CreateMap<CreateRiskImpactForRiskCommand, CreateServiceRiskImpactArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        ;

        CreateMap<CreateThreatCommand, CreateThreatArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        ;

        CreateMap<CreateRiskCommand, CreateRiskRelatedIssueArg>()
      .ForMember(x => x.IssueId, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
      .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
      .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));
    }
}
