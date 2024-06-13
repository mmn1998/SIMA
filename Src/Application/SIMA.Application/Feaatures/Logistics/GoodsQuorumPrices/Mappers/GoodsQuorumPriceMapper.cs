using AutoMapper;
using SIMA.Application.Contract.Features.Logistics.GoodsQuorumPrices;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Logistics.GoodsQuorumPrices.Mappers;

public class GoodsQuorumPriceMapper : Profile
{
    public GoodsQuorumPriceMapper()
    {
        CreateMap<CreateGoodsQuorumPriceCommand, CreateGoodsQuorumPriceArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyGoodsQuorumPriceCommand, ModifyGoodsQuorumPriceArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
