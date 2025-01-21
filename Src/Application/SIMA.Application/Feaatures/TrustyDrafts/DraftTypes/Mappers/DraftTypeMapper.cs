using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftTypes;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftTypes.Mappers;

public class DraftTypeMapper : Profile
{
    public DraftTypeMapper()
    {
        CreateMap<CreateDraftTypeCommand, CreateDraftTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyDraftTypeCommand, ModifyDraftTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}