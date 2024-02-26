using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.Profiles;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;

namespace SIMA.Application.Query.Features.Auths.AddressTypes.Mappers;

public class AddressTypeQueryMapper : Profile
{
    public AddressTypeQueryMapper()
    {
        CreateMap<AddressType, GetAddressBookQueryResult>();
    }
}
