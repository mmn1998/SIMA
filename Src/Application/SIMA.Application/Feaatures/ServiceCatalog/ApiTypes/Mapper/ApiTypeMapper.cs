using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.ApiTypes;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.ApiTypes.Mapper;

public class ApiTypeMapper : Profile
{
    public ApiTypeMapper()
    {
        CreateMap<CreateApiTypeCommand, CreateApiTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            ;
        CreateMap<ModifyApiTypeCommand, ModifyApiTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}