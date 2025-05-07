using AutoMapper;
using SIMA.Application.Contract.Features.BCP.Senarios;
using SIMA.Application.Contract.Features.Notifications.Messages;
using SIMA.Domain.Models.Features.BCP.Scenarios.Args;
using SIMA.Domain.Models.Features.Notifications.Messages.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Notifications.Messages.Mapper
{
    public class MessageMapper : Profile
    {
        public MessageMapper()
        {
            CreateMap<CreateMessageCommand, CreateMessageArg>()
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ExpireDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ExpireDate)))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            ;

            CreateMap<CreateNotificationMessageGroupDisplay, CreateMessageGroupDisplayArg>()
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            ;

            CreateMap<CreateNotificationMessagePositionDisplay, CreateMessagePositionDisplayArg>()
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            ;

            CreateMap<CreateNotificationMessageAttachmentDisplay, CreateMessageAttachmentArg>()
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            ;

            CreateMap<ModifyMessageCommand, ModifyMessageArg>()
                 .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                 .ForMember(dest => dest.ExpireDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ExpireDate)))
                 .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));

            CreateMap<CreateMessageSeenStatisticsCommand, CreateMessageSeenStatisticsArg>()
                 .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
                 .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                 .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));
        }
    }
}
