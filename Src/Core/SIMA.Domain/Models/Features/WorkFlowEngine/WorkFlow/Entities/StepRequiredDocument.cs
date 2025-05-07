using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;

public class StepRequiredDocument : Entity
{
    private StepRequiredDocument()
    {
    }
    private StepRequiredDocument(CreateStepRequiredDocumentArg arg)
    {
        Id  = new StepRequiredDocumentId(IdHelper.GenerateUniqueId());
        StepId = new StepId(arg.StepId);
        DocumentTypeId = new DocumentTypeId(arg.DocumentTypeId);
        Count = arg.Count;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedBy = arg.CreatedBy;
        CreatedAt = arg.CreatedAt;
    }
    public static StepRequiredDocument New(CreateStepRequiredDocumentArg arg)
    {
        return new StepRequiredDocument(arg);
    }
    public static StepRequiredDocument Create(CreateStepRequiredDocumentArg arg)
    {
        return new StepRequiredDocument(arg);
    }
    public async Task Modify(CreateStepRequiredDocumentArg arg)
    {
        Count = arg.Count;
        ModifiedBy = arg.CreatedBy;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
    }
    public async Task ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public StepRequiredDocumentId Id { get; private set; }
    public StepId StepId { get; private set; }
    public virtual Step Step { get; private set; }
    public DocumentTypeId DocumentTypeId { get; private set; }
    public virtual DocumentType DocumentType { get; private set; }
    public int Count { get; private set; }

    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
