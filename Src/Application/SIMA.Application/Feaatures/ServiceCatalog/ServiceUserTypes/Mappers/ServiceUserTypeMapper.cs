using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceUserTypes;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceUserTypes.Mappers;

public class ServiceUserTypeMapper : Profile
{
    public ServiceUserTypeMapper()
    {
        CreateMap<CreateServiceUserTypeCommand, CreateServiceUserTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyServiceUserTypeCommand, ModifyServiceUserTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}