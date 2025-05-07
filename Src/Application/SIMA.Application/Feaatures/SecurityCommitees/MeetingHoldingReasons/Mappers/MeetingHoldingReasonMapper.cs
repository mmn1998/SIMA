using AutoMapper;
using SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingReasons;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.SecurityCommitees.MeetingHoldingReasons.Mappers;

public class MeetingHoldingReasonMapper : Profile
{   
    public MeetingHoldingReasonMapper()
    {
        CreateMap<CreateMeetingHoldingReasonCommand, CreateMeetingHoldingReasonArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            ;
        CreateMap<ModifyMeetingHoldingReasonCommand, ModifyMeetingHoldingReasonArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
