using System.Text;
using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.ThreatTypes;
using SIMA.Application.Contract.Features.RiskManagers.TriggerStatuses;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Args;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Args;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Feaatures.RiskManagers.TriggerStatuses.Mapper;

public class TriggerStatusMapper: Profile
{
    public TriggerStatusMapper()
    {
        CreateMap<CreateTriggerStatusCommand, CreateTriggerStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyTriggerStatusCommand, ModifyTriggerStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }

}