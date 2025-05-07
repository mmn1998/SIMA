using AutoMapper;
using SIMA.Application.Contract.Features.WorkFlowEngine.ApprovalOptions;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.WorkFlowEngine.ApprovalOptions.Mapper
{
    public class ApprovalOptionMapper : Profile
    {
        public ApprovalOptionMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateApprovalOptionCommand, CreateApprovalOptionArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<ModifyApprovalOptionCommand, ModifyApprovalOptionArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.ModifyBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        }
    }
}
