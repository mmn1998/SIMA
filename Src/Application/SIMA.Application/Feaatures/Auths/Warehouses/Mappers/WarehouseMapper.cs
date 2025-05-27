using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Warehouses;
using SIMA.Domain.Models.Features.Auths.Warehouses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.Warehouses.Mappers;

public class WarehouseMapper : Profile
{
    public WarehouseMapper()
    {
        CreateMap<CreateWarehouseCommand, CreateWarehouseArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyWarehouseCommand, ModifyWarehouseArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}