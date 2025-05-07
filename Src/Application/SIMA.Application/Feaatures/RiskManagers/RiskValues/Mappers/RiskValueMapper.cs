using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.RiskValues;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.RiskValues.Mappers;

public class RiskValueMapper : Profile
{
    public RiskValueMapper()
    {
        CreateMap<CreateRiskValueCommand, CreateRiskValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyRiskValueCommand, ModifyRiskValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}