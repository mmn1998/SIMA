using System.Text;
using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.MatrixAValues;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Args;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Feaatures.RiskManagers.MatrixAValues.Mapper;

public class MatrixAValueMapper : Profile
{
    public MatrixAValueMapper()
    {
        CreateMap<CreateMatrixAValuesCommand, CreateMatrixAValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyMatrixAValuesCommand, ModifyMatrixAValueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));

    }
}