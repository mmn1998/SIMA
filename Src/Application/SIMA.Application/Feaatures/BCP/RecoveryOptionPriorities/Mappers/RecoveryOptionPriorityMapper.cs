using AutoMapper;
using SIMA.Application.Contract.Features.BCP.RecoveryOptionPriorities;
using SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.RecoveryOptionPriorities.Mappers;

public class RecoveryOptionPriorityMapper : Profile
{
    public RecoveryOptionPriorityMapper()
    {
        CreateMap<CreateRecoveryOptionPriorityCommand, CreateRecoveryOptionPriorityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyRecoveryOptionPriorityCommand, ModifyRecoveryOptionPriorityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}