using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceBoundles;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceBoundles.Mappers;

public class ServiceBoundleMapper : Profile
{
    public ServiceBoundleMapper()
    {
        CreateMap<CreateServiceBoundleCommand, CreateServiceBoundleArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyServiceBoundleCommand, ModifyServiceBoundleArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
