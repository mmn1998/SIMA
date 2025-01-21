using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.ReferralLetters;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.ReferralLetters.Mapper;

public class ReferralLetterMapper : Profile
{
    public ReferralLetterMapper()
    {
        CreateMap<CreateReferralLetterCommand, CreateReferalLetterArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.LetterDate, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
           ;

        CreateMap<ModifyReferralLetterCommand, ModifyReferalLetterArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.LetterDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.LetterDate)))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
