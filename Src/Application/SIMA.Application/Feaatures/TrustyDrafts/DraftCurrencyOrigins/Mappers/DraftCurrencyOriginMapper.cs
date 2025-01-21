using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftCurrencyOrigins;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftCurrencyOrigins.Mappers;

public class DraftCurrencyOriginMapper : Profile
{
    public DraftCurrencyOriginMapper()
    {
        CreateMap<CreateDraftCurrencyOriginCommand, CreateDraftCurrencyOriginArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyDraftCurrencyOriginCommand, ModifyDraftCurrencyOriginArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}