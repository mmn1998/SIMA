using AutoMapper;
using SIMA.Application.Contract.Features.SecurityCommitees.Meetings;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;

namespace SIMA.Application.Feaatures.SecurityCommitees.Meetings.Mapper
{
    public class MeetingMapper : Profile
    {
        private static ISimaIdentity _simaIdentity;

        public MeetingMapper(ISimaIdentity simaIdentity)
        {
            _simaIdentity = simaIdentity;

            CreateMap<CreateMeetingCommands, CreateMeetingArg>()
           .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
           .ForMember(x => x.IssueId, opt => opt.MapFrom(src =>  IdHelper.GenerateUniqueId()))
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.Code, act => act.MapFrom(source => CreateCode() + "/" + source.MeetingTurn))
           .ForMember(dest => dest.Labels, act => act.MapFrom(source => GetLabels(source.Lable)))
           .ForMember(dest => dest.NewSubject, act => act.MapFrom(source => GetSubject(source.NewSubject)));

            
            CreateMap<long, CreateMeetingLabelArg>()
            .ForMember(dest => dest.LabelId, act => act.MapFrom(source => source))
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));


            CreateMap<MeetingDocumentCommand, CreateMeetingDocumentArg>()
          .ForMember(dest => dest.DocumentId, act => act.MapFrom(source => source.DocumentId))
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<MeetingReasonsCommand, CreateMeetingReasonArg>()
          .ForMember(dest => dest.MeetingHoldingReasonId, act => act.MapFrom(source => source.ReasonId))
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

         //   CreateMap<SubjectCommand, CreateSubjectArg>()
         //.ForMember(dest => dest.IsArchived, act => act.MapFrom(source => "0"))
         //.ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
         ////.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
         //.ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<ExistsSubjectcommand, CreateSubjectMeetingArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));
        }
        private List<string> GetLabels(string labels)
        {
            return labels.Split(",").ToList();
        }
        public List<CreateSubjectArg> GetSubject(List<CreateSubjectCommand> subjectCommands)
        {
            var subjectArgs = new List<CreateSubjectArg>();
            foreach (var command in subjectCommands)
            {
                var subject = new CreateSubjectArg
                {
                    ActiveStatusId = (long)ActiveStatusEnum.Active,
                    CreatedAt = DateTime.Now,
                    CreatedBy = _simaIdentity.UserId,
                    Id = IdHelper.GenerateUniqueId(),
                    Title = command.Title,
                    Description = command.Description,
                };
                subjectArgs.Add(subject);
            }
            return subjectArgs;
        }

        private string CreateCode()
        {
            var persiaDateTime = DateHelper.ToPersianDate(DateTime.Now);
            var year= persiaDateTime.Split('/').ToList();
            return year[0];
        }
    }


}
