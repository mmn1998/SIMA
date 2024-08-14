using AutoMapper;
using SIMA.Application.Contract.Features.Auths.ApiMethodActions;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.ApiMethodActions.Mappers;

public class ApiMethodActionMapper : Profile
{
    public ApiMethodActionMapper()
    {
        CreateMap<CreateApiMethodActionCommand, CreateApiMethodActionArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyApiMethodActionCommand, ModifyApiMethodActionArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}