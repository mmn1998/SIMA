using SIMA.Domain.Models.Features.Auths.MainAggregates.Entities;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.Args;
using SIMA.Domain.Models.Features.DMS.Documents.Interfaces;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.DMS.Documents.Entities;

public class Document : Entity
{
    private Document() { }
    private Document(CreateDocumentArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        if (arg.MainAggregateId.HasValue) MainAggregateId = new(arg.MainAggregateId.Value);
        SourceId = arg.SourceId;
        AttachStepId = new(arg.AttachStepId);
        FileAddress = arg.FileAddress;
        FileExtensionId = new(arg.FileExtensionId);
        DocumentTypeId = new(arg.DocumentTypeId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<Document> Create(CreateDocumentArg arg, IDocumentDomainService service)
    {
        await CreateGuards(arg, service);
        return new Document(arg);
    }
    public async Task Modify(ModifyDocumentArg arg, IDocumentDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        if (arg.MainAggregateId.HasValue) MainAggregateId = new(arg.MainAggregateId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        SourceId = arg.SourceId;
        AttachStepId = new(arg.AttachStepId);
        FileExtensionId = new(arg.FileExtensionId);
        DocumentTypeId = new(arg.DocumentTypeId);
        FileAddress = arg.FileAddress;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateDocumentArg arg, IDocumentDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyDocumentArg arg, IDocumentDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    #endregion
    public DocumentId Id { get; private set; }
    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public MainAggregateId? MainAggregateId { get; private set; }
    public virtual MainAggregate? MainAggregate { get; private set; }
    public long? SourceId { get; private set; }
    public StepId? AttachStepId { get; private set; }
    public virtual Step? AttachStep { get; private set; }
    public DocumentTypeId DocumentTypeId { get; private set; }
    public virtual DocumentType DocumentType { get; private set; }
    public DocumentExtensionId FileExtensionId { get; private set; }
    public virtual DocumentExtension FileExtension { get; private set; }
    public string? FileAddress { get; private set; }
    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public ICollection<IssueDocument> IssueDocuments { get; set; }
    public ICollection<ApprovalResponsibleAnswerDocument> ApprovalResponsibleAnswerDocuments { get; set; }
    public ICollection<ApprovalSupervisorAnswerDocument> ApprovalSupervisorAnswerDocuments { get; set; }
    public ICollection<MeetingDocument> MeetingDocuments { get; set; }
    public ICollection<BusinessImpactAnalysisDocument> BusinessImpactAnalysisDocuments { get; set; }
    public ICollection<BusinessContinuityStrategyDocument> BusinessContinuityStrategyDocuments { get; set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
