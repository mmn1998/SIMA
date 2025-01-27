using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.ReconsilationTypes;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.ReconsilationTypes.Mappers;

public class ReconsilationTypeMapper : Profile
{
    public ReconsilationTypeMapper()
    {
        CreateMap<CreateReconsilationTypeCommand, CreateReconsilationTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyReconsilationTypeCommand, ModifyReconsilationTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}