using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.AffectedHistories;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.AffectedHistories.Mappers;

public class AffectedHistoryMapper : Profile
{
    public AffectedHistoryMapper()
    {
        CreateMap<CreateAffectedHistoryCommand, CreateAffectedHistoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyAffectedHistoryCommand, ModifyAffectedHistoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}