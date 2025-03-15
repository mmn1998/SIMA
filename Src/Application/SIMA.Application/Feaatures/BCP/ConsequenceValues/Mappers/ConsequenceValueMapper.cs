using AutoMapper;
using SIMA.Application.Contract.Features.BCP.ConsequenceValues;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.ConsequenceValues.Mappers;

public class ConsequenceValueMapper : Profile
{
    public ConsequenceValueMapper()
    {
        CreateMap<CreateConsequenceValueCommand, CreateConsequenceValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyConsequenceValueCommand, ModifyConsequenceValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}