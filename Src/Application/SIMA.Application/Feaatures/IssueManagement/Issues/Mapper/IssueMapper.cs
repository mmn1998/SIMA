using AutoMapper;
using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Globalization;
using System.Text;

namespace SIMA.Application.Feaatures.IssueManagement.Issues.Mapper
{
    public class IssueMapper : Profile
    {
        public IssueMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateIssueCommand, CreateIssueArg>()
                .ForMember(x => x.Id, act => act.MapFrom(src => IdHelper.GenerateUniqueId()))
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
                .ForMember(dest => dest.DueDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.DueDate)))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<CreateIssueCommand, CreateIssueHistoryArg>()
                .ForMember(x => x.Id, act => act.MapFrom(src => IdHelper.GenerateUniqueId()))
                .ForMember(x => x.CreatedBy, act => act.MapFrom(src => simaIdentity.UserId))
                .ForMember(x => x.PerformerUserId, act => act.MapFrom(src => simaIdentity.UserId))
                .ForMember(x => x.CreatedAt, act => act.MapFrom(src => DateTime.Now))
                .ForMember(x => x.Description, act => act.MapFrom(src => src.Description))
                .ForMember(x => x.Name, act => act.MapFrom(src => src.Summery));

            CreateMap<Issue, CreateIssueHistoryArg>()
               .ForMember(x => x.Id, act => act.MapFrom(src => IdHelper.GenerateUniqueId()))
               .ForMember(x => x.PerformerUserId, act => act.MapFrom(src => simaIdentity.UserId))
               .ForMember(x => x.CreatedBy, act => act.MapFrom(src => simaIdentity.UserId))
               .ForMember(x => x.CreatedAt, act => act.MapFrom(src => DateTime.Now))
               .ForMember(x => x.Description, act => act.MapFrom(src => src.Description))
               .ForMember(x => x.Name, act => act.MapFrom(src => src.Summery));

            CreateMap<ModifyIssueCommand, ModifyIssueArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));

            CreateMap<IssueRunActionCommand, IssueRunActionArg>()
            .ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));

            CreateMap<CreateIssueLinkCommand, CreateIssueLinkArg>()
               .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
               .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
               .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));


            CreateMap<CreateIssueDocumentCommand, CreateIssueDocumentArg>()
               .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
              .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
               .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<CreateIssueCommentCommand, CreateIssueCommentArg>()
               .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
               .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
               .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));


            CreateMap<IssueRunActionCommand, CreateIssueCommentArg>()
              .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
              .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
              .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        }

        //public DateTime ConvertToDateTime(string persianDate)
        //{
        //    try
        //    {
        //        string[] parts = persianDate.Split('/');
        //        int year = int.Parse(parts[0]);
        //        int month = int.Parse(parts[1]);
        //        int day = int.Parse(parts[2]);

        //        // تبدیل تاریخ فارسی به تاریخ میلادی
        //        PersianCalendar pc = new PersianCalendar();
        //        DateTime dateTime = pc.ToDateTime(year, month, day, 0, 0, 0, 0);
        //        return dateTime;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw IssueExceptions.IssueDueDateError;

        //    }

        //}

    }



}
