using AutoMapper;
using SIMA.Application.Contract.Features.BCP.ServicePriorities;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.ServicePriorities.Mapper;

public class OrganizationalServicePriorityMapper : Profile
{
    public OrganizationalServicePriorityMapper()
    {
        CreateMap<CreateOrganizationalServicePriorityCommand, CreateOrganizationalServicePriorityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyOrganizationalServicePriorityCommand, ModifyOrganizationalServicePriorityArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}