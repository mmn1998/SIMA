using AutoMapper;
using SIMA.Application.Contract.Features.BCP.HappeningPossiblities;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.HappeningPossiblities.Mappers;

public class HappeningPossibilityMapper : Profile
{
    public HappeningPossibilityMapper()
    {
        CreateMap<CreateHappeningPossibilityCommand, CreateHappeningPossibilityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyHappeningPossibilityCommand, ModifyHappeningPossibilityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
