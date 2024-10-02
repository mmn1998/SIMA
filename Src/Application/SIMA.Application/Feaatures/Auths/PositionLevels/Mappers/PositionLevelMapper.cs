using AutoMapper;
using SIMA.Application.Contract.Features.Auths.PositionLevels;
using SIMA.Domain.Models.Features.Auths.PositionLevels.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.PositionLevels.Mappers;

public class PositionLevelMapper : Profile
{
    public PositionLevelMapper()
    {
        CreateMap<CreatePositionLevelCommand, CreatePositionLevelArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyPositionLevelCommand, ModifyPositionLevelArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}