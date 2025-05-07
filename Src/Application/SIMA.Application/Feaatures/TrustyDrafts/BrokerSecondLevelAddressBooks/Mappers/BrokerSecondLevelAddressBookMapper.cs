using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.BrokerSecondLevelAddressBooks.Mappers;

public class BrokerSecondLevelAddressBookMapper : Profile
{
    public BrokerSecondLevelAddressBookMapper()
    {
        CreateMap<CreateBrokerSecondLevelAddressBookCommand, CreateBrokerSecondLevelAddressBookArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyBrokerSecondLevelAddressBookCommand, ModifyBrokerSecondLevelAddressBookArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}