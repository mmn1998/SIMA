using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.InherentOccurrenceProbabilityValues;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.InherentOccurrenceProbabilityValues.Mappers;

public class InherentOccurrenceProbabilityValueMapper : Profile
{
    public InherentOccurrenceProbabilityValueMapper()
    {
        CreateMap<CreateInherentOccurrenceProbabilityValueCommand, CreateInherentOccurrenceProbabilityValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyInherentOccurrenceProbabilityValueCommand, ModifyInherentOccurrenceProbabilityValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}