using System.Text;
using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.Frequencies;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.Args;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Feaatures.RiskManagers.Frequencies.Mapper;

public class FrequencyMapper: Profile
{
    public FrequencyMapper()
    {
        CreateMap<CreateFrequencyCommand, CreateFrequencyArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyFrequencyCommand, ModifyFrequencyArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}