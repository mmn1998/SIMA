using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.CurrencyPaymentChannels;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.CurrencyPaymentChannels.Mappers;

public class CurrencyPaymentChannelMapper : Profile
{
    public CurrencyPaymentChannelMapper()
    {
        CreateMap<CreateCurrencyPaymentChannelCommand, CreateCurrencyPaymentChannelArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyCurrencyPaymentChannelCommand, ModifyCurrencyPaymentChannelArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}