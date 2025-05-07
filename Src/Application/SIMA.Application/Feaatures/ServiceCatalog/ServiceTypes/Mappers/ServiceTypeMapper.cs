using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceTypes;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceTypes.Mappers;

public class ServiceTypeMapper : Profile
{
    public ServiceTypeMapper()
    {
        CreateMap<CreateServiceTypeCommand, CreateServiceTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            ;
        CreateMap<ModifyServiceTypeCommand, ModifyServiceTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
