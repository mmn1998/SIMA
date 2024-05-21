using AutoMapper;
using SIMA.Application.Contract.Features.Auths.PhoneTypes;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.PhoneTypes.Mappers;

public class PhoneTypeMapper : Profile
{
    public PhoneTypeMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreatePhoneTypeCommand, CreatePhoneTypeArg>()
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
                .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));
        ;

        CreateMap<ModifyPhoneTypeCommand, ModifyPhoneTypeArg>()
          .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString())))
          //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
           ;
    }
}
