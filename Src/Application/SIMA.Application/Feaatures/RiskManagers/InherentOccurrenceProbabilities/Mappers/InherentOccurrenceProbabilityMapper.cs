using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.InherentOccurrenceProbabilities;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.InherentOccurrenceProbabilities.Mappers;

public class InherentOccurrenceProbabilityMapper : Profile
{
    public InherentOccurrenceProbabilityMapper()
    {
        CreateMap<CreateInherentOccurrenceProbabilityCommand, CreateInherentOccurrenceProbabilityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyInherentOccurrenceProbabilityCommand, ModifyInherentOccurrenceProbabilityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}