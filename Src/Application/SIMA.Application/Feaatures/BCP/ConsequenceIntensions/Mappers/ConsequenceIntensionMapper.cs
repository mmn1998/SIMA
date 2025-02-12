using AutoMapper;
using SIMA.Application.Contract.Features.BCP.ConsequenceIntensions;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.ConsequenceIntensions.Mappers;

public class ConsequenceIntensionMapper : Profile
{
    public ConsequenceIntensionMapper()
    {
        CreateMap<CreateConsequenceIntensionCommand, CreateConsequenceIntensionArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyConsequenceIntensionCommand, ModifyConsequenceIntensionArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}