using AutoMapper;
using SIMA.Application.Contract.Features.Auths.UIInputElements;
using SIMA.Domain.Models.Features.Auths.UIInputElements.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.UIInputElements.Mappers;

public class UIInputElementMapper : Profile
{
    public UIInputElementMapper()
    {
        CreateMap<CreateUIInputElementCommand, CreateUIInputElementArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyUIInputElementCommand, ModifyUIInputElementArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
