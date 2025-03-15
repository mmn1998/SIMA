using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Back_UpMethods;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.Back_UpMethods.Mappers;

public class BackupMethodMapper : Profile
{
    public BackupMethodMapper()
    {
        CreateMap<CreateBackupMethodCommand, CreateBackupMethodArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyBackupMethodCommand, ModifyBackupMethodArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}