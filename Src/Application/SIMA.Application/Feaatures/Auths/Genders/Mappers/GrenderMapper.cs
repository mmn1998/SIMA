
using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Genders;
using SIMA.Domain.Models.Features.Auths.Genders.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.Genders.Mappers;

public class GenderMapper : Profile
{
    public GenderMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateGenderCommand, CreateGenderArg>()
         .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
        //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
         .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
         .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));


        CreateMap<ModifyGenderCommand, ModifyGenderArg>()
           .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
           //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
           ;
    }
}
