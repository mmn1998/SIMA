
using AutoMapper;
using SIMA.Application.Contract.Features.Auths.LocationTypes;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.LocationTypes.Mappers;

public class LocationTypeMapper : Profile
{
    public LocationTypeMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateLocationTypeCommand, CreateLocationTypeArg>()
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
            //.ForMember(x => x.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));
        ;

        CreateMap<ModifyLocationTypeCommand, ModifyLocationTypeArg>()
           .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            //.ForMember(x => x.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
    }
}
