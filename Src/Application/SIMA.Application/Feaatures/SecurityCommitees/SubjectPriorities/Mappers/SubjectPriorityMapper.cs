using AutoMapper;
using SIMA.Application.Contract.Features.SecurityCommitees.SubjectPriorities;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.SecurityCommitees.SubjectPriorities.Mappers;

public class SubjectPriorityMapper : Profile
{
    public SubjectPriorityMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateSubjectPriorityCommand, CreateSubjectPriorityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            ;
        CreateMap<ModifySubjectPriorityCommand, ModifySubjectPriorityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
