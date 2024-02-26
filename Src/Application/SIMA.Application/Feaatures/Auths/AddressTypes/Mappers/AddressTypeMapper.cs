using AutoMapper;
using SIMA.Application.Contract.Features.Auths.AddressTypes;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.AddressTypes.Mappers;

public class AddressTypeMapper : Profile
{
    public AddressTypeMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateAddressTypeCommand, CreateAddressTypeArg>()
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
                .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => (long)ActiveStatusEnum.Active))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));

        CreateMap<ModifyAddressTypeCommand, ModifyAddressTypeArg>()
                .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString())))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
        ;
    }

}
