using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.InquiryRequests;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.InquiryRequests.Mapper;

public class InquiryRequestMapper : Profile
{

    public InquiryRequestMapper()
    {
        CreateMap<CreateInquiryRequestCommand, CreateInquiryRequestArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.Description, act => act.MapFrom(source => source.RequestDescription))
            //.ForMember(dest => dest.ProformaDate, act => act.MapFrom(source => source.ProformaDate.ToMiladiDate()))
            .ForMember(dest => dest.DraftOrderDate, act => act.MapFrom(source => source.DraftOrderDate.ToMiladiDate()))
            ;
        CreateMap<CreateInquiryRequestDocumentCommand, CreateInquiryRequestDocumentArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<CreateInquiryRequestCurrencyCommand, CreateInquiryRequestCurrencyArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyInquiryRequestCommand, ModifyInquiryRequestArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
