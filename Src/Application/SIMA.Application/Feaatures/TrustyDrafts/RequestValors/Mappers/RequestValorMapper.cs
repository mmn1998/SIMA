using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.RequestValors;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.RequestValors.Mappers;

public class RequestValorMapper : Profile
{
    public RequestValorMapper()
    {
        CreateMap<CreateRequestValorCommand, CreateRequestValorArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyRequestValorCommand, ModifyRequestValorArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}