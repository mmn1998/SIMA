using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.ResponsibilityWageTypes;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.ResponsibilityWageTypes.Mappers;

public class ResponsibilityWageTypeMapper : Profile
{
    public ResponsibilityWageTypeMapper()
    {
        CreateMap<CreateResponsibilityWageTypeCommand, CreateResponsibilityWageTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyResponsibilityWageTypeCommand, ModifyResponsibilityWageTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}