using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.RiskLevelCobits;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.RiskLevelCobits.Mappers;

public class RiskLevelCobitMapper : Profile
{
    public RiskLevelCobitMapper()
    {
        CreateMap<CreateRiskLevelCobitCommand, CreateRiskLevelCobitArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyRiskLevelCobitCommand, ModifyRiskLevelCobitArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}