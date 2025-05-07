using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItems;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.ConfigurationItems.Mappers;

public class ConfigurationItemMapper : Profile
{
    public ConfigurationItemMapper()
    {

        CreateMap<CreateConfigurationItemCommand, CreateConfigurationItemArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.LastUpdateDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.LastUpdateDate)))
            .ForMember(dest => dest.ReleaseDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ReleaseDate)))
            .ForMember(dest => dest.IssueId, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyConfigurationItemCommand, ModifyConfigurationItemArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.LastUpdateDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.LastUpdateDate)))
            .ForMember(dest => dest.ReleaseDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ReleaseDate)))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
        CreateMap<CreateConfigurationItemCustomFieldValueCommand, CreateConfigurationItemCustomFieldValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ItemValue, act => act.MapFrom(source => source.Value))
            ;
        CreateMap<CreateConfigurationItemSupportTeamCommand, CreateConfigurationItemSupportTeamArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;

        //CreateMap<CreateConfigurationItemBackupScheduleInfo, CreateConfigurationItemBackupScheduleArg>()
        //    .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        //    .ForMember(dest => dest.LastTestDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.LastTestDate)))
        //    .ForMember(dest => dest.StartTime, act => act.MapFrom(source => DateHelper.ToTimeOnly(source.StartTime)))
        //    .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        //    .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        //    ;
        CreateMap<CreateConfigurationItemAccessInfoCommand, CreateConfigurationItemAccessInfoArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ActiveFrom, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ActiveFrom)))
            .ForMember(dest => dest.ActiveTo, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ActiveTo)))
            ;
        CreateMap<CreateConfigurationItemBackupScheduleInfo, CreateConfigurationItemBackupScheduleArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.LastTestDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.LastTestDate)))
            .ForMember(dest => dest.StartTime, act => act.MapFrom(source => DateHelper.ToTimeOnly(source.StartTime)))
            ;
        CreateMap<CreateConfigurationItemApiCommand, CreateConfigurationItemApiArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<CreateConfigurationItemDataProcedureCommand, CreateConfigurationItemDataProcedureArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<CreateServiceConfigurationItemCommand, CreateServiceConfigurationItemArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<CreateConfigurationItemDocumentCommand, CreateConfigurationItemDocumentArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<CreateConfigurationItemAssetCommand, CreateConfigurationItemAssetArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()));

             CreateMap<CreateConfigurationItemArg, CreateConfigurationItemIssueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        
    }
}
