using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.WageDeductionMethods;
using SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.WageDeductionMethods.Mappers;

public class WageDeductionMethodMapper : Profile
{
    public WageDeductionMethodMapper()
    {
        CreateMap<CreateWageDeductionMethodCommand, CreateWageDeductionMethodArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyWageDeductionMethodCommand, ModifyWageDeductionMethodArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}