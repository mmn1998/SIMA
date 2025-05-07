using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceStatuses;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceStatuses.Mapper
{
    public class ServiceStatusMapper : Profile
    {
        public ServiceStatusMapper()
        {
            CreateMap<CreateServiceStatusCommand, CreateServiceStatusArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
                .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
                ;
            CreateMap<ModifyServiceStatusCommand, ModifyServiceStatusArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
                ;
        }
    }
}
