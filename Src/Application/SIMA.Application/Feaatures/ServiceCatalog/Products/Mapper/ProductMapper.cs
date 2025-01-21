using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.Products;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.Products.Mapper;

public class ProductMapper :Profile
{
    public ProductMapper()
    {
        CreateMap<CreateProductCommand, CreateProductArg>()
              .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
              .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
              .ForMember(dest => dest.InServiceDate, act => act.MapFrom(source => GetDate(source.InServiceDate)))
              .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
              ;
        CreateMap<ModifyProductCommand, ModifyProductArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.InServiceDate, act => act.MapFrom(source => GetDate(source.InServiceDate)))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;

        CreateMap<CreateChannelList, CreateProductChannelArg>()
         .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
         .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
         .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
         ;

        CreateMap<ProductResponsibleCommand, CreateProductResponsibleArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;
        CreateMap<ProductChannelCommand, CreateProductChannelArg>()
       .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
       .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
       .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
       ;

    }
    private DateOnly? GetDate(string? persianDate)
    {
        DateOnly? value = null;
        var date = persianDate.ToMiladiDate();
        if (date.HasValue)
        {
            value = DateOnly.FromDateTime(date.Value);
        }
        return value;
    }
}
