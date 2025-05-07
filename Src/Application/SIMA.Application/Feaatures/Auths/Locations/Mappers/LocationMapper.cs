
using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Locations;
using SIMA.Domain.Models.Features.Auths.Locations.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.Locations.Mappers;

public class LocationMapper : Profile
{
    public LocationMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateLocationCommand, CreateLocationArg>()
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active))
            //.ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));
        ;

        CreateMap<ModifyLocationCommand, ModifyLocationArg>()
           .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
           //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
        ;

    }
}
