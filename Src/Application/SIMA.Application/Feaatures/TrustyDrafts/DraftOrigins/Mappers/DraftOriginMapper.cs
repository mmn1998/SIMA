using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftOrigins;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftOrigins.Mappers;

public class DraftOriginMapper : Profile
{
    public DraftOriginMapper()
    {
        CreateMap<CreateDraftOriginCommand, CreateDraftOriginArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyDraftOriginCommand, ModifyDraftOriginArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}