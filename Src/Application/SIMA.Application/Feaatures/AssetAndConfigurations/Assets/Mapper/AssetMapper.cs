using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Framework.Common.Helper;
using System.Text;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.Assets.Mapper;

public class AssetMapper : Profile
{
    public AssetMapper()
    {
        CreateMap<CreateAssetCommand, CreateAssetArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ExpireDate, act => act.MapFrom(source => DateHelper.ConvertPersianToDateOnly(source.ExpireDate)))
            .ForMember(dest => dest.IssueId, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ManufactureDate, act => act.MapFrom(source => DateHelper.ConvertPersianToDateOnly(source.ManufactureDate)))
            .ForMember(dest => dest.OwnershipDate, act => act.MapFrom(source => DateHelper.ConvertPersianToDateOnly(source.OwnershipDate)))
            .ForMember(dest => dest.RetiredDate, act => act.MapFrom(source => DateHelper.ConvertPersianToDateOnly(source.RetiredDate)))
            ;
        CreateMap<ModifyAssetCommand, ModifyAssetArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ExpireDate, act => act.MapFrom(source => DateHelper.ConvertPersianToDateOnly(source.ExpireDate)))
            .ForMember(dest => dest.ManufactureDate, act => act.MapFrom(source => DateHelper.ConvertPersianToDateOnly(source.ManufactureDate)))
            .ForMember(dest => dest.OwnershipDate, act => act.MapFrom(source => DateHelper.ConvertPersianToDateOnly(source.OwnershipDate)))
            .ForMember(dest => dest.RetiredDate, act => act.MapFrom(source => DateHelper.ConvertPersianToDateOnly(source.RetiredDate)))
            ;
        CreateMap<long, CreateServiceAssetArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ServiceId, act => act.MapFrom(source => source))
            ;
        CreateMap<long, CreateAssetDocumentArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.DocumentId, act => act.MapFrom(source => source))
            ;
        CreateMap<long, CreateConfigurationItemAssetArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ConfigurationItemId, act => act.MapFrom(source => source))
            ;
        CreateMap<long, CreateComplexAssetArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ParentAssetId, act => act.MapFrom(source => source))
            ;
        CreateMap<CreateAssetAssignedStaffCommand, CreateAssetAssignedStaffArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<CreateAssetArg, CreateAssetIssueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;


        CreateMap<CreateAssetCustomFeildValueCommand, CreateAssetCustomFieldValueArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
           ;
    }
}
