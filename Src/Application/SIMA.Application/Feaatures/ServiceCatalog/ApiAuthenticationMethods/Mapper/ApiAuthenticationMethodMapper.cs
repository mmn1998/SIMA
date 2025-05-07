using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.ApiAuthenticationMethods;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.ApiAuthenticationMethods.Mapper
{
    public class ApiAuthenticationMethodMapper : Profile
    {
        public ApiAuthenticationMethodMapper()
        {
            CreateMap<CreateApiAuthenticationMethodCommand, CreateApiAuthenticationMethodArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
                ;
            CreateMap<ModifyApiAuthenticationMethodCommand, ModifyApiAuthenticationMethodArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
                ;
        }
    }
}
