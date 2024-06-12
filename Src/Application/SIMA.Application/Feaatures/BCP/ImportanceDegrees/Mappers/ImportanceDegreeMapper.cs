using AutoMapper;
using SIMA.Application.Contract.Features.BCP.ImportanceDegrees;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.ImportanceDegrees.Mappers;

public class ImportanceDegreeMapper : Profile
{
    public ImportanceDegreeMapper()
    {
        CreateMap<CreateImportanceDegreeCommand, CreateImportanceDegreeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyImportanceDegreeCommand, ModifyImportanceDegreeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}