using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Suppliers;
using SIMA.Domain.Models.Features.Auths.Suppliers.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.Suppliers.Mappers;

public class SupplierMapper : Profile
{
    public SupplierMapper()
    {
        CreateMap<CreateSupplierCommand, CreateSupplierArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;

        CreateMap<CreateSupplierAccountListCommand, CreateSupplierAccountListArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           ;

        CreateMap<CreateSupplierAddressBookCommand, CreateSupplierAddressBookArg>()
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          ;

        CreateMap<CreateSupplierPhoneBookCommand, CreateSupplierPhoneBookArg>()
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          ;

        CreateMap<ModifySupplierCommand, ModifySupplierArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
        CreateMap<long, CreateSupplierDocumentArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.DocumentId, act => act.MapFrom(source => source))
            ;
    }
}
