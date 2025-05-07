using AutoMapper;
using SIMA.Application.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.IssueManagement.IssueLinkReasons.Mapper
{
    public class IssueLinkReasonMapper : Profile
    {
        public IssueLinkReasonMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateIssueLinkReasonCommand, CreateIssueLinkReasonArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));
            CreateMap<ModifyIssueLinkReasonCommand, ModifyIssueLinkReasonArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        }
    }
}
