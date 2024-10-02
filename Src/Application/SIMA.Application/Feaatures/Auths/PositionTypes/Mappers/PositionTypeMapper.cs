using AutoMapper;
using SIMA.Application.Contract.Features.Auths.PositionTypes;
using SIMA.Domain.Models.Features.Auths.PositionTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.PositionTypes.Mappers;

public class PositionTypeMapper : Profile
{
    public PositionTypeMapper()
    {
        CreateMap<CreatePositionTypeCommand, CreatePositionTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyPositionTypeCommand, ModifyPositionTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}