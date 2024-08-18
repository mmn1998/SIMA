using AutoMapper;
using SIMA.Application.Contract.Features.Auths.CustomerTypes;
using SIMA.Domain.Models.Features.Auths.ServiceCustomerTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.CustomerTypes.Mappers;

public class CustomerTypeMapper : Profile
{
    public CustomerTypeMapper()
    {
        CreateMap<CreateCustomerTypeCommand, CreateCustomerTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyCustomerTypeCommand, ModifyCustomerTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}