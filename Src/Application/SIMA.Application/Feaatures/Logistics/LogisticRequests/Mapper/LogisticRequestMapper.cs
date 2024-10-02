using AutoMapper;
using SIMA.Application.Contract.Features.Logistics.LogisticRequests;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Logistics.LogisticRequests.Mapper
{
    public class LogisticRequestMapper : Profile
    {
        public LogisticRequestMapper()
        {
            CreateMap<CreateLogisticRequestCommand, CreateLogisticsRequestArg>()
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.IssuePreorityId, act => act.MapFrom(source => source.IssueInforamation.IssuePriorityId))
            .ForMember(dest => dest.Weight, act => act.MapFrom(source => source.IssueInforamation.Weight))
            .ForMember(dest => dest.OwnerUserId, act => act.MapFrom(source => source.IssueInforamation.OwnerUserId))
            //.ForMember(dest => dest.DueDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.IssueInforamation.DueDate)))
            .ForMember(x => x.IssueId, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));


            CreateMap<LogisticRequestGoodsCommand, CreateLogisticsRequestGoodsArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<LogisticRequestDocumentCommand, CreateLogisticsRequestDocumentArg>()
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<ModifyLogisticsRequestCommand, ModifyLogisticsRequestArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            .ForMember(dest => dest.IssuePreorityId , act => act.MapFrom(source => source.IssueInforamation.IssuePriorityId))
            .ForMember(dest => dest.Weight , act => act.MapFrom(source => source.IssueInforamation.Weight))
            .ForMember(dest => dest.DueDate , act => act.MapFrom(source => DateHelper.ToMiladiDate(source.IssueInforamation.DueDate)));
           
        }
    }
}
