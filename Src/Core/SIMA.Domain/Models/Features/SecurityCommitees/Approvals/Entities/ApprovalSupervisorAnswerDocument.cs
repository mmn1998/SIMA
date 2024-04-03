using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;

public class ApprovalSupervisorAnswerDocument
{
    private ApprovalSupervisorAnswerDocument() { }
    private ApprovalSupervisorAnswerDocument(CreateApprovalSupervisorAnswerDocumentArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        DocumentId = new(arg.DocumentId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ApprovalSupervisorAnswerDocument> Create(CreateApprovalSupervisorAnswerDocumentArg arg,
        IApprovalResponsibleAnswerDocumentDomainService service)
    {
        await CreateGuards(arg, service);
        return new ApprovalSupervisorAnswerDocument(arg);
    }
    public async Task Modify(ModifyApprovalSupervisorAnswerDocumentArg arg, IApprovalResponsibleAnswerDocumentDomainService service)
    {
        await ModifyGuards(arg, service);
        DocumentId = new(arg.DocumentId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateApprovalSupervisorAnswerDocumentArg arg, IApprovalResponsibleAnswerDocumentDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyApprovalSupervisorAnswerDocumentArg arg, IApprovalResponsibleAnswerDocumentDomainService service)
    {

    }
    #endregion
    public ApprovalSupervisorAnswerDocumentId Id { get; private set; }
    public DocumentId DocumentId { get; private set; }
    public virtual Document Document { get; private set; }
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

