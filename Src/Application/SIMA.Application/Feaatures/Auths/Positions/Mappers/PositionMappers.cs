using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Positions;
using SIMA.Domain.Models.Features.Auths.Positions.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.Positions.Mappers;

public class PositionMappers : Profile
{
    public PositionMappers(ISimaIdentity simaIdentity)
    {
        CreateMap<CreatePositionCommand, CreatePositionArg>()
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));


        CreateMap<ModifyPositionCommand, ModifyPositionArg>()
            .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString())))
            .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
    }
}
