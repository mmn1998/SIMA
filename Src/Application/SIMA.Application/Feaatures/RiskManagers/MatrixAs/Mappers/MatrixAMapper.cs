using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.MatrixAs;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.MatrixAs.Mappers;

public class MatrixAMapper : Profile
{
    public MatrixAMapper()
    {
        CreateMap<CreateMatrixACommand, CreateMatrixAArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyMatrixACommand, ModifyMatrixAArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}