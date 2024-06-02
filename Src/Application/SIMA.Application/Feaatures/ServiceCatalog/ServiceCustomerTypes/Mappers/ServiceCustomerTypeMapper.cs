using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceCustomerTypes;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceCustomerTypes.Mappers;

public class ServiceCustomerTypeMapper : Profile
{
    public ServiceCustomerTypeMapper()
    {
        CreateMap<CreateServiceCustomerTypeCommand, CreateServiceCustomerTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyServiceCustomerTypeCommand, ModifyServiceCustomerTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}