using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.AgentBankWageShareStatuses;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.AgentBankWageShareStatuses.Mappers;

public class AgentBankWageShareStatusMapper : Profile
{
    public AgentBankWageShareStatusMapper()
    {
        CreateMap<CreateAgentBankWageShareStatusCommand, CreateAgentBankWageShareStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyAgentBankWageShareStatusCommand, ModifyAgentBankWageShareStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
