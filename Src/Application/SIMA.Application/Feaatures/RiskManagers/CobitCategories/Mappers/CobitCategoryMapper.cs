using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.CobitCategories;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.RiskManagers.CobitCategories.Mappers;

public class CobitCategoryMapper : Profile
{
    public CobitCategoryMapper()
    {
        CreateMap<CreateCobitCategoryCommand, CreateCobitCategoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyCobitCategoryCommand, ModifyCobitCategoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}