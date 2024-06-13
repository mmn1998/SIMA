using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.ChannelTypes;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceBoundles;
using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.ServiceCatalog.ChannelTypes.Mappers;

public class ChannelTypeMapper : Profile
{
    public ChannelTypeMapper()
    {
        CreateMap<CreateChannelTypeCommand, CreateChannelTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyChannelTypeCommand, ModifyChannelTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}