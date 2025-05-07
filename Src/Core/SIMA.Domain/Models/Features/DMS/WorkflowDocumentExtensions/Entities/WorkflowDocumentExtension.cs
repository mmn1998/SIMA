using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.ValueObjects;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Args;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Interfaces;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Entities;

public class WorkflowDocumentExtension : Entity
{
    private WorkflowDocumentExtension() { }
    private WorkflowDocumentExtension(CreateWorkflowDocumentExtensionArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        WorkflowId = arg.WorkflowId;
        DocumentExtensionId = new(arg.DocumentExtensionId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<WorkflowDocumentExtension> Create(CreateWorkflowDocumentExtensionArg arg, IWorkflowDocumentExtensionDomainService service)
    {
        await CreateGuards(arg, service);
        return new WorkflowDocumentExtension(arg);
    }
    public async Task Modify(ModifyWorkFlowDocumentExtensionArg arg, IWorkflowDocumentExtensionDomainService service)
    {
        await ModifyGuards(arg, service);
        WorkflowId = arg.WorkflowId;
        DocumentExtensionId = new(arg.DocumentExtensionId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    public static async Task CreateGuards(CreateWorkflowDocumentExtensionArg arg, IWorkflowDocumentExtensionDomainService service)
    {
        arg.NullCheck();
        arg.WorkflowId.NullCheck();
        arg.DocumentExtensionId.NullCheck();
    }
    public async Task ModifyGuards(ModifyWorkFlowDocumentExtensionArg arg, IWorkflowDocumentExtensionDomainService service)
    {
        arg.NullCheck();
        arg.WorkflowId.NullCheck();
        arg.DocumentExtensionId.NullCheck();
    }
    #endregion
    public WorkflowDocumentExtensionId Id { get; private set; }

    public long WorkflowId { get; private set; }
    public DocumentExtensionId DocumentExtensionId { get; private set; }
    public virtual DocumentExtension DocumentExtension { get; private set; }
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
