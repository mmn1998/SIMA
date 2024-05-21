using AutoMapper;
using SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingStatus;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.SecurityCommitees.MeetingHoldingStatus.Mapper
{
    public class MeetingHoldingStatusMapper : Profile
    {
        public MeetingHoldingStatusMapper(ISimaIdentity simaIdentity)
        {

            CreateMap<CreateMeetingHoldingStatusCommand, CreateMeetingHoldingStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            ;
            CreateMap<ModifyMeetingHoldingStatusCommand, ModifyMeetingHoldingStatusArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
                ;
        }
    }
}
