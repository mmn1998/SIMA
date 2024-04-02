using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;

public class ApprovalResponsibleAnswerDocument
{
    private ApprovalResponsibleAnswerDocument() { }
    private ApprovalResponsibleAnswerDocument(CreateApprovalResponsibleAnswerDocumentArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        DocumentId = new(arg.DocumentId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ApprovalResponsibleAnswerDocument> Create(CreateApprovalResponsibleAnswerDocumentArg arg,
        IApprovalResponsibleAnswerDocumentDomainService service)
    {
        await CreateGuards(arg, service);
        return new ApprovalResponsibleAnswerDocument(arg);
    }
    public async Task Modify(ModifyApprovalResponsibleAnswerDocumentArg arg, IApprovalResponsibleAnswerDocumentDomainService service)
    {
        await ModifyGuards(arg, service);
        DocumentId = new(arg.DocumentId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateApprovalResponsibleAnswerDocumentArg arg, IApprovalResponsibleAnswerDocumentDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyApprovalResponsibleAnswerDocumentArg arg, IApprovalResponsibleAnswerDocumentDomainService service)
    {

    }
    #endregion
    public ApprovalResponsibleAnswerId Id { get; private set; }
    public DocumentId DocumentId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
