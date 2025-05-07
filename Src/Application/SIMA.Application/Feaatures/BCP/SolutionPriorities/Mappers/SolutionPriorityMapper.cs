using AutoMapper;
using SIMA.Application.Contract.Features.BCP.SolutionPriorities;
using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.SolutionPriorities.Mappers;

public class SolutionPriorityMapper : Profile
{
    public SolutionPriorityMapper()
    {
        CreateMap<CreateSolutionPriorityCommand, CreateSolutionPriorityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifySolutionPriorityCommand, ModifySolutionPriorityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}