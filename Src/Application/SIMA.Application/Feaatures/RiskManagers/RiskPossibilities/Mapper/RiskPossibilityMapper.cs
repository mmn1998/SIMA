using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.RiskPossibilities;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.RiskPossibilities.Mapper
{
    public class RiskPossibilityMapper : Profile
    {
        public RiskPossibilityMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateRiskPossibilityCommand, CreateRiskPossibilityArgs>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<ModifyRiskPossibilityCommand, ModifyRiskPossibilityArgs>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.ModifyBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        }
    }
}
