using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.ImpactScales;
using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.ImpactScales.Mapper
{
    public class ImpactScaleMapper : Profile
    {
        public ImpactScaleMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateImpactScaleCommand, CreateImpactScaleArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<ModifyImpactScaleCommand, ModifyImpactScaleArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.ModifyBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        }
    }
}
