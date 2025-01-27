using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.CurrentOccurrenceProbabilities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.CurrentOccurrenceProbabilities.Mappers;

public class CurrentOccurrenceProbabilityMapper : Profile
{
    public CurrentOccurrenceProbabilityMapper()
    {
        CreateMap<CreateCurrentOccurrenceProbabilityCommand, CreateCurrentOccurrenceProbabilityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyCurrentOccurrenceProbabilityCommand, ModifyCurrentOccurrenceProbabilityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}