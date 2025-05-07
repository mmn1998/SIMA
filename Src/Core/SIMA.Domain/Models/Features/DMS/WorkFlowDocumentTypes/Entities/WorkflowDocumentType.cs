using SIMA.Domain.Models.Features.DMS.DocumentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.ValueObjects;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Args;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Interfaces;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Entities;

public class WorkflowDocumentType : Entity
{
    private WorkflowDocumentType() { }
    private WorkflowDocumentType(CreateWorkFlowDocumentTypeArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        WorkflowId = arg.WorkflowId;
        DocumentTypeId = new(arg.DocumentTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<WorkflowDocumentType> Create(CreateWorkFlowDocumentTypeArg arg, IWorkflowDocumentTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new WorkflowDocumentType(arg);
    }
    public async Task Modify(ModifyWorkFlowDocumentTypeArg arg, IWorkflowDocumentTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        WorkflowId = arg.WorkflowId;
        DocumentTypeId = new(arg.DocumentTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    public static async Task CreateGuards(CreateWorkFlowDocumentTypeArg arg, IWorkflowDocumentTypeDomainService service)
    {
        arg.NullCheck();
        arg.WorkflowId.NullCheck();
        arg.DocumentTypeId.NullCheck();
    }
    public async Task ModifyGuards(ModifyWorkFlowDocumentTypeArg arg, IWorkflowDocumentTypeDomainService service)
    {
        arg.NullCheck();
        arg.WorkflowId.NullCheck();
        arg.DocumentTypeId.NullCheck();
    }
    #endregion
    public WorkflowDocumentTypeId Id { get; private set; }

    public long WorkflowId { get; private set; }
    public DocumentTypeId DocumentTypeId { get; private set; }
    public virtual DocumentType DocumentType { get; private set; }
    public long ActiveStatusId { get; private set; }


    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
