using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.CurrentOccurrenceProbabilityValues;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.CurrentOccurrenceProbabilityValues.Mappers;

public class CurrentOccurrenceProbabilityValueMapper : Profile
{
    public CurrentOccurrenceProbabilityValueMapper()
    {
        CreateMap<CreateCurrentOccurrenceProbabilityValueCommand, CreateCurrentOccurrenceProbabilityValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyCurrentOccurrenceProbabilityValueCommand, ModifyCurrentOccurrenceProbabilityValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}