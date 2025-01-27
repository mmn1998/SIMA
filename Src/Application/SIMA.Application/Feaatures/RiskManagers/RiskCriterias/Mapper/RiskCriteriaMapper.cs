using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.RiskCriterias;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.RiskCriterias.Mapper;

public class RiskCriteriaMapper : Profile
{
    public RiskCriteriaMapper()
    {
        CreateMap<CreateRiskCriteriaCommand, CreateRiskCriteriaArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyRiskCriteriaCommand, ModifyRiskCriteriaArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.ModifyBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}
