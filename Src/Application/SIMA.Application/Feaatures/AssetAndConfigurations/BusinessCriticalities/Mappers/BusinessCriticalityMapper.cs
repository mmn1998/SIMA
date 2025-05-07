using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.BusinessCriticalities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.BusinessCriticalities.Mappers;

public class BusinessCriticalityMapper : Profile
{
    public BusinessCriticalityMapper()
    {
        CreateMap<CreateBusinessCriticalityCommand, CreateBusinessCriticalityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyBusinessCriticalityCommand, ModifyBusinessCriticalityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}