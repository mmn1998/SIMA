using System.Text;
using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Categories;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.Args;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.Categories.Mapper;

public class CategoryMapper: Profile
{
    public CategoryMapper()
    {
        CreateMap<CreateCategoryCommand, CreateCategoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyCategoryCommand, ModifyCategoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}