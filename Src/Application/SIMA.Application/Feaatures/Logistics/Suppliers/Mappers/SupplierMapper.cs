using AutoMapper;
using SIMA.Application.Contract.Features.Logistics.Suppliers;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Logistics.Suppliers.Mappers;

public class SupplierMapper : Profile
{
    public SupplierMapper()
    {
        CreateMap<CreateSupplierCommand, CreateSupplierArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifySupplierCommand, ModifySupplierArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
