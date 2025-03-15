using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.DataProcedureTypes;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.DataProcedureTypes.Mappers;

public class DataProcedureTypeMapper : Profile
{
    public DataProcedureTypeMapper()
    {
        CreateMap<CreateDataProcedureTypeCommand, CreateDataProcedureTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyDataProcedureTypeCommand, ModifyDataProcedureTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}