using AutoMapper;
using SIMA.Application.Contract.Features.DMS.WorkFlowDocumentExtentions;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.DMS.WorkFlowDocumentExtentions.Mappers
{
    public class WorkFlowDocumentExtentionsMapper : Profile
    {
        public WorkFlowDocumentExtentionsMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateWorkFlowDocumentExtentionCommand, CreateWorkflowDocumentExtensionArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                    .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                    .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));
            CreateMap<ModifyWorkFlowDocumentExtentionCommand, ModifyWorkFlowDocumentExtensionArg>()
                .ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
                    .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        }
    }
}
