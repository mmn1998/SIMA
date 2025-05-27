using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Apis;
using SIMA.Application.Contract.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.Apis.Mappers;

public class ApiMapper : Profile
{
    public ApiMapper()
    {
        CreateMap<CreateApiCommand, CreateApiArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyApiCommand, ModifyApiArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
        CreateMap<CreateDraftDocument, CreateTrustyDraftDocumentArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;

        CreateMap<CreateApiDocument, CreateApiDocumentArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;
        CreateMap<CreateApiSupportTeam, CreateApiSupportTeamArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;
        CreateMap<CreateConfigurationItemApi, CreateConfigurationItemApiArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;

        CreateMap<CreateApiRequestHeaderParam, CreateApiRequestHeaderParamArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;

        CreateMap<CreateApiRequestBodyParam, CreateApiRequestBodyParamArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;

        CreateMap<CreateApiResponseBodyParam, CreateApiResponseBodyParamArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;

        CreateMap<CreateApiResponseHeaderParam, CreateApiResponseHeaderParamArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;

        CreateMap<CreateApiRequestUrlParam, CreateApiRequestUrlParamArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;

        CreateMap<CreateApiRequestQueryStringParam, CreateApiRequestQueryStringParamArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;

        CreateMap<ModifyApiCommand, ModifyApiArg>()
             .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
       ;
    }
}