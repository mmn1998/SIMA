
using System.Text;
using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.OperationalStatuses;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Args;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.OperationalStatuses.Mapper;

public class OperationalStatusMapper: Profile
{
    public OperationalStatusMapper()
    {
        CreateMap<CreateOperationalStatusCommand, CreateOperationalStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyOperationalStatusCommand, ModifyOperationalStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
        
    }
}