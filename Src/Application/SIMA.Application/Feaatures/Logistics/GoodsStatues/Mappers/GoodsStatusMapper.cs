using AutoMapper;
using SIMA.Application.Contract.Features.Logistics.GoodsStatues;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Logistics.GoodsStatues.Mappers;

public class GoodsStatusMapper : Profile
{
    public GoodsStatusMapper()
    {
        CreateMap<CreateGoodsStatusCommand, CreateGoodsStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyGoodsStatusCommand, ModifyGoodsStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}