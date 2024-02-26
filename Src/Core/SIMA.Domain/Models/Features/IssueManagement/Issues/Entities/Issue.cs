using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

public class Issue : Entity
{
    private Issue() { }
    private Issue(CreateIssueArg arg)
    {
        Id = new IssueId(arg.Id);
        Code = arg.Code;
        Summery = arg.Summery;
        CurrentWorkflowId = arg.CurrentWorkflowId;
        CurrentStateId = arg.CurrentStateId;
        CurrenStepId = arg.CurrenStepId;
        MainAggregateId = arg.MainAggregateId;
        SourceId = arg.SourceId;
        IssueTypeId = new(arg.IssueTypeId);
        IssuePriorityId = new(arg.IssuePriorityId);
        IssueWeightCategoryId = new(arg.IssueWeightCategoryd);
        Weight = arg.Weight;
        IssueDate = arg.IssueDate;
        Description = arg.Description;
        DueDate = arg.DueDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        CompanyId = arg.CompanyId;
    }

    public static async Task<Issue> Create(CreateIssueArg arg, IIssueDomainService service)
    {
        await GuardAgainstCodeUniqueness(arg.Id, arg.Code, service);
        await CreateGuards(arg, service);

        return new Issue(arg);
    }

    private static async Task GuardAgainstCodeUniqueness(long id, string code,IIssueDomainService service)
    {
        var isCodeUnique = await service.IsCodeUnique(code, id);
        if (isCodeUnique)
        {
            throw IssueExceptions.IssueCodeIsNotUnique;
        }
    }

    public void AddHistory(CreateIssueHistoryArg arg)
    {
        _issueHistories.Add(IssueHistory.Create(arg));
    }

    public async Task Modify(ModifyIssueArg arg, IIssueDomainService service)
    {
        await ModifyGuards(arg, service);
        IssueTypeId = new(arg.IssueTypeId);
        IssuePriorityId = new(arg.IssuePriorityId);
        IssueWeightCategoryId = new(arg.IssueWeightCategoryd);
        Weight = arg.Weight;
        Description = arg.Description;
        Summery = arg.Summery;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    public async void RunAction(IssueRunActionArg arg)
    {
        CurrentStateId = arg.CurrentStateId;
        CurrenStepId = arg.CurrentStepId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public async Task AddComment(CreateIssueCommentArg issueCommentArg)
    {
        var comment = await IssueComment.Create(issueCommentArg);
        _issueComments.Add(comment);
    }
    public void DeactiveComment(IssueCommentId issueCommentId)
    {
        var comment = _issueComments.FirstOrDefault(c => c.Id == issueCommentId);
        comment.NullCheck();
        comment?.Deactive();
    }
    public bool DeactiveIssueLink(long issueLinkId)
    {
        var result = _issueLink.Where(x => x.Id == new IssueLinkId(issueLinkId)).FirstOrDefault();
        if (result is not null)
        {
            result.Deactive();
            return true;
        }
        else
            return false;

    }
    public bool DeactiveIssueDocument(long issueDocumentId)
    {
        var result = _issueDocuments.Where(x => x.Id == new IssueDocumentId(issueDocumentId)).FirstOrDefault();
        if (result is not null)
        {
            result.Deactive();
            return true;
        }
        else
            return false;

    }


    public async void AddIssueLink(List<CreateIssueLinkArg> issueLinkArgs)
    {
        foreach (var item in issueLinkArgs)
        {
            if(item.IssueIdLinkedTo > 0)
            {
                item.IssueId = Id.Value;
                var issuelink = await IssueLink.Create(item);
                _issueLink.Add(issuelink);
            }
           
        }
    }
    public async void AddIssueDocument(List<CreateIssueDocumentArg> issueDocumentArgs)
    {
        foreach (var item in issueDocumentArgs)
        {
            if(item.DocumentId > 0)
            {
                var issueDocument = await IssueDocument.Create(item);
                _issueDocuments.Add(issueDocument);
            }
            
        }
    }


    public IssueId Id { get; private set; }
    public long CompanyId { get; private set; }
    public long CurrentWorkflowId { get; private set; }
    public string Code { get; private set; }
    public string Summery { get; private set; }
    public long CurrentStateId { get; private set; }
    public long CurrenStepId { get; private set; }
    public long MainAggregateId { get; private set; }
    public long SourceId { get; private set; }
    public IssueTypeId IssueTypeId { get; private set; }
    public virtual IssueType IssueType { get; private set; }
    public IssuePriorityId IssuePriorityId { get; private set; }
    public virtual IssuePriority IssuePriority { get; private set; }
    public IssueWeightCategoryId IssueWeightCategoryId { get; private set; }
    public virtual IssueWeightCategory IssueWeightCategory { get; private set; }
    public int Weight { get; private set; }
    public DateTime IssueDate { get; private set; }
    public string? Description { get; private set; }
    public DateTime DueDate { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;
    }
    private List<IssueComment> _issueComments = new();
    public ICollection<IssueComment> IssueComments => _issueComments;

    private List<IssueDocument> _issueDocuments = new();
    public ICollection<IssueDocument> IssueDocuments => _issueDocuments;

    private List<IssueLink> _issueLink = new();
    public ICollection<IssueLink> IssueLinks => _issueLink;
    private List<IssueHistory> _issueHistories = new();
    public ICollection<IssueHistory> IssueHistories => _issueHistories;
    public ICollection<IssueLink> IssuesLinkedTo { get; private set; }

    #region Gaurds

    private static async Task CreateGuards(CreateIssueArg arg, IIssueDomainService service)
    {
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (arg.Code.Length > 20) throw SimaResultException.LengthCodeException;
    }
    private async Task ModifyGuards(ModifyIssueArg arg, IIssueDomainService service)
    {
        arg.ActiveStatusId.NullCheck();
    }
    #endregion
}


