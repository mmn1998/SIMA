using AutoMapper;
using SIMA.Application.Contract.Features.BCP.RecoveryPointObjectives;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.RecoveryPointObjectives.Mappers;

public class RecoveryPointObjectiveMapper : Profile
{
    public RecoveryPointObjectiveMapper()
    {
        CreateMap<CreateRecoveryPointObjectiveCommand, CreateRecoveryPointObjectiveArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyRecoveryPointObjectiveCommand, ModifyRecoveryPointObjectiveArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}