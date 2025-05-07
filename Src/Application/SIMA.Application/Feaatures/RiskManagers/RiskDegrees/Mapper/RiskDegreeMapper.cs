using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.RiskDegrees;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.RiskDegrees.Mapper
{
    public class RiskDegreeMapper : Profile
    {
        public RiskDegreeMapper()
        {
            CreateMap<CreateRiskDegreeCommand, CreateRiskDegreeArgs>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<ModifyRiskDegreeCommand, ModifyRiskDegreeArgs>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.ModifyBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        }
    }
}
