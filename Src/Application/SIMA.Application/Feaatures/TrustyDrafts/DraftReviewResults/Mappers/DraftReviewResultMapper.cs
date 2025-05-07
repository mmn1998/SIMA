using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftReviewResults;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftReviewResults.Mappers;

public class DraftReviewResultMapper : Profile
{
    public DraftReviewResultMapper()
    {
        CreateMap<CreateDraftReviewResultCommand, CreateDraftReviewResultArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyDraftReviewResultCommand, ModifyDraftReviewResultArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}