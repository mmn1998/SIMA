using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftIssueTypes;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftIssueTypes.Mappers;

public class DraftIssueTypeMapper : Profile
{
    public DraftIssueTypeMapper()
    {
        CreateMap<CreateDraftIssueTypeCommand, CreateDraftIssueTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyDraftIssueTypeCommand, ModifyDraftIssueTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}