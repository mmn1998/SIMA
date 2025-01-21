using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.ServicePriorities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriorities.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServicePriorities.Mapper
{
    public class ServicePriorityMapper : Profile
    {
        public ServicePriorityMapper()
        {
            CreateMap<CreateServicePriorityCommand, CreateServicePriorityArg>()
                  .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                  .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
                  ;
            CreateMap<ModifyServicePriorityCommand, ModifyServicePriorityArgs>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
                ;

        }
    }
}
