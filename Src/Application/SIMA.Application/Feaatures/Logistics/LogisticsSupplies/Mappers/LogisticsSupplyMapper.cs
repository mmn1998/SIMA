using AutoMapper;
using SIMA.Application.Contract.Features.Logistics.LogisticsSupplies;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Logistics.LogisticsSupplies.Mappers;

public class LogisticsSupplyMapper : Profile
{
    public LogisticsSupplyMapper()
    {
        CreateMap<CreateLogisticsSupplyCommand, CreateLogisticsSupplyArg>()
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        .ForMember(dest => dest.IssuePreorityId, act => act.MapFrom(source => source.IssueInforamation.IssuePriorityId))
        //.ForMember(dest => dest.RequesterId, act => act.MapFrom(source => source.IssueInforamation.RequesterId))
        //.ForMember(dest => dest.DueDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.IssueInforamation.DueDate)))
        .ForMember(x => x.IssueId, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
        ;

        CreateMap<ModifyLogisticsSupplyCommand, ModifyLogisticsSupplyArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
        .ForMember(dest => dest.IssuePreorityId, act => act.MapFrom(source => source.IssueInforamation.IssuePriorityId))
        //.ForMember(dest => dest.DueDate , act => act.MapFrom(source => DateHelper.ToMiladiDate(source.IssueInforamation.DueDate)))
        ;
        CreateMap<long, CreateLogisticsSupplyDocumentArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        .ForMember(dest => dest.DocumentId, act => act.MapFrom(source => source ))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        ;
        CreateMap<long, CreateLogisticsSupplyGoodsArg>()
        .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        .ForMember(dest => dest.LogisticsRequestGoodsId, act => act.MapFrom(source => source))
        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        ;

    }
}
