using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.ConsequenceCategories;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.ConsequenceCategories.Mappers;

public class ConsequenceCategoryMapper : Profile
{
    public ConsequenceCategoryMapper()
    {
        CreateMap<CreateConsequenceCategoryCommand, CreateConsequenceCategoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyConsequenceCategoryCommand, ModifyConsequenceCategoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.ModifyBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}
