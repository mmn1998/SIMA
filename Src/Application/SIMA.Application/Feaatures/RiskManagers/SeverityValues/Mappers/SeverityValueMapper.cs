using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.SeverityValues;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.SeverityValues.Mappers;

public class SeverityValueMapper : Profile
{
    public SeverityValueMapper()
    {
        CreateMap<CreateSeverityValueCommand, CreateSeverityValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifySeverityValueCommand, ModifySeverityValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}