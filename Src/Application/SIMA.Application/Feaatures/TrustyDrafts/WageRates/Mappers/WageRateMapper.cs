using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.WageRates;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.WageRates.Mappers;

public class WageRateMapper : Profile
{
    public WageRateMapper()
    {
        CreateMap<CreateWageRateCommand, CreateWageRateArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyWageRateCommand, ModifyWageRateArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}