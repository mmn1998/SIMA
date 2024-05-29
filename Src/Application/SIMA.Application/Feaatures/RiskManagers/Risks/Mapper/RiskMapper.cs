using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.Risks;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Feaatures.RiskManagers.Risks.Mapper
{
    public class RiskMapper : Profile
    {
        public RiskMapper()
        {
            CreateMap<CreateRiskCommand, CreateRiskArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<CreateRiskCommand, CreateEffectedAssetArgs>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));


            CreateMap<CreateCorrectiveActionCommand, CreateCorrectiveActionArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<CreatePreventiveActionCommand, CreatePreventiveActionArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<CreateRiskCommand, CreateRiskRelatedIssueArg>()
          .ForMember(x => x.IssueId, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));
        }
    }
}
