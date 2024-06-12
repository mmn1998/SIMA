using AutoMapper;
using SIMA.Application.Contract.Features.BCP.Consequences;
using SIMA.Domain.Models.Features.BCP.Consequences.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.Consequences.Mappers;

public class ConsequenceMapper : Profile
{
    public ConsequenceMapper()
    {
        CreateMap<CreateConsequenceCommand, CreateConsequenceArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyConsequenceCommand, ModifyConsequenceArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}