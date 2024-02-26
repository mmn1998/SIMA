using AutoMapper;
using SIMA.Application.Contract.Features.IssueManagement.IssuePriorities;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.IssueManagement.IssuePriorities.Mappers;

public class IssuePriorityMapper : Profile
{
    public IssuePriorityMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateIssuePriorityCommand, CreateIssuePriorityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));
        CreateMap<ModifyIssuePriorityCommand, ModifyIssuePriorityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}
