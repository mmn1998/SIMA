using AutoMapper;
using SIMA.Application.Contract.Features.Logistics.Goods;
using SIMA.Domain.Models.Features.Logistics.Goodses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Logistics.Goodses.Mappers;

public class GoodsMapper : Profile
{
    public GoodsMapper()
    {
        CreateMap<CreateGoodsCommand, CreateGoodsArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyGoodsCommand, ModifyGoodsArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}