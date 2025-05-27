using AutoMapper;
using SIMA.Application.Contract.Features.BCP.ConsequenceIntensionDescriptions;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.ConsequenceIntensionDescriptions.Mappers;

public class ConsequenceIntensionDescriptionMapper : Profile
{
    public ConsequenceIntensionDescriptionMapper()
    {
        CreateMap<CreateConsequenceIntensionDescriptionCommand, CreateConsequenceIntensionDescriptionArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyConsequenceIntensionDescriptionCommand, ModifyConsequenceIntensionDescriptionArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}