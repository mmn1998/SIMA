using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.CancellationResaons;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.CancellationResaons.Mappers;

public class CancellationResaonMapper : Profile
{
    public CancellationResaonMapper()
    {
        CreateMap<CreateCancellationResaonCommand, CreateCancellationResaonArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyCancellationResaonCommand, ModifyCancellationResaonArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}