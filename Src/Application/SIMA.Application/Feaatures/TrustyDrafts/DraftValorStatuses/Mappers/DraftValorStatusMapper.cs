using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftValorStatuses;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftValorStatuses.Mappers;

public class DraftValorStatusMapper : Profile
{
    public DraftValorStatusMapper()
    {
        CreateMap<CreateDraftValorStatusCommand, CreateDraftValorStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyDraftValorStatusCommand, ModifyDraftValorStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}