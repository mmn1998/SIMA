using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

public class IssueDocument : Entity
{
    private IssueDocument()
    {
    }

    public IssueDocument(CreateIssueDocumentArg arg)
    {
        Id = new IssueDocumentId(IdHelper.GenerateUniqueId());
        IssueId = new IssueId(arg.IssueId);
        DocumentId = arg.DocumentId;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<IssueDocument> Create(CreateIssueDocumentArg arg)
    {
        return new IssueDocument(arg);
    }
    public async void Modify(ModifyIssueDocumentArg arg)
    {
        IssueId = new IssueId(arg.IssueId);
        DocumentId = arg.DocumentId;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public IssueDocumentId Id { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public long DocumentId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;
    }
}
