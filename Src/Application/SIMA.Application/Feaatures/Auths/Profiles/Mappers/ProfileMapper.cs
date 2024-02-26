
using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Profiles;
using SIMA.Domain.Models.Features.Auths.Profiles.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;

namespace SIMA.Application.Feaatures.Auths.Profiles.Mappers;

public class ProfileMapper : Profile
{
    public ProfileMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateProfileCommand, CreateProfileArg>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));
        ;
        CreateMap<CreateAddressBookCommand, CreateAddressBookArg>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<ModifyAddressBookCommand, ModifyAddressBookArg>()
            .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;

        CreateMap<CreatePhoneBookCommand, CreatePhoneBookArg>()
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
    }
}
