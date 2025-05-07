using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.BrokerInquiryStatuses;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.BrokerInquiryStatuses.Mapper;

public class BrokerInquiryStatusMapper : Profile
{
    public BrokerInquiryStatusMapper()
    {
        CreateMap<CreateBrokerInquiryStatusCommand, CreateBrokerInquiryStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyBrokerInquiryStatusCommand, ModifyBrokerInquiryStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}