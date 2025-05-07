using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceCategories.Mappers;

public class ServiceCategoryMapper : Profile
{
    public ServiceCategoryMapper()
    {
        CreateMap<CreateServiceCategoryCommand, CreateServiceCategoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyServiceCategoryCommand, ModifyServiceCategoryArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}