using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.LicenseStatuses;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.LicenseStatuses.Mappers;

public class LicenseStatusMapper : Profile
{
    public LicenseStatusMapper()
    {
        CreateMap<CreateLicenseStatusCommand, CreateLicenseStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyLicenseStatusCommand, ModifyLicenseStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}