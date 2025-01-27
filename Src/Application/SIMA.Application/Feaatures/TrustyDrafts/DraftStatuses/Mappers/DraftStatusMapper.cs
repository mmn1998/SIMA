using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftStatuses;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftStatuses.Mappers;

public class DraftStatusMapper : Profile
{
    public DraftStatusMapper()
    {
        CreateMap<CreateDraftStatusCommand, CreateDraftStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyDraftStatusCommand, ModifyDraftStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}