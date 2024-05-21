using AutoMapper;
using SIMA.Application.Contract.Features.DMS.DocumentExtensions;
using SIMA.Application.Contract.Features.DMS.DocumentTypes;
using SIMA.Application.Contract.Features.DMS.WorkFlowDocumentTypes;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Args;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.DMS.WorkFlowDocumentTypes.Mappers;

public class WorkFlowDocumentTypesMapper : Profile
{
    public WorkFlowDocumentTypesMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateWorkflowDocumentTypeCommand, CreateWorkFlowDocumentTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));
        CreateMap<ModifyWorkflowDocumentTypeCommand, ModifyWorkFlowDocumentTypeArg>()
            //.ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}
