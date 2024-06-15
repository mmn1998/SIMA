using AutoMapper;
using SIMA.Application.Contract.Features.Logistics.GoodsTypes;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Logistics.GoodsTypes.Mappers;

public class GoodsTypeMapper : Profile
{
    public GoodsTypeMapper()
    {
        CreateMap<CreateGoodsTypeCommand, CreateGoodsTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyGoodsTypeCommand, ModifyGoodsTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}