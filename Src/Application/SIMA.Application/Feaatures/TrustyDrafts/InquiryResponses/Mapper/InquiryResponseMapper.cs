using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.InquiryResponses;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.InquiryResponses.Mapper;

public class InquiryResponseMapper : Profile
{
    public InquiryResponseMapper()
    {
        CreateMap<CreateInquiryResponseCommand, CreateInquiryResponseArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.ValidityPeriod, act => act.Ignore())
            //.ForMember(dest => dest.ValidityPeriod, act => act.MapFrom(source => DateTime.Now.AddMonths(1)))
            //.ForMember(dest => dest.ValidityPeriod, act => act.MapFrom(source => source.ValidityPeriod.ToMiladiDate()))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.Description, act => act.MapFrom(source => source.ResponseDescription))
            ;
        CreateMap<ModifyInquiryResponseCommand, ModifyInquiryResponseArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            //.ForMember(dest => dest.ValidityPeriod, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ValidityPeriod)))
            ;
    }
}
