using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.RiskTypes;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.RiskTypes.Mapper;

public class RiskTypeMapper : Profile
{
    public RiskTypeMapper()
    {
        CreateMap<CreateRiskTypeCommand, CreateRiskTypeArgs>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyRiskTypeCommand, ModifyRiskTypeArgs>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.ModifyBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}
