using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;

public class ApprovalResponsibleAnswer
{
    private ApprovalResponsibleAnswer() { }
    private ApprovalResponsibleAnswer(CreateApprovalResponsibleAnswerArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        ApprovalId = new(arg.ApprovalId);
        ResponsibleAnswerTypeId = new(arg.ResponsibleAnswerTypeId);
        Description = arg.Description;
        ReportDate = arg.ReportDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ApprovalResponsibleAnswer> Create(CreateApprovalResponsibleAnswerArg arg, IApprovalResponsibleAnswerDomainService service)
    {
        await CreateGuards(arg, service);
        return new ApprovalResponsibleAnswer(arg);
    }
    public async Task Modify(ModifyApprovalResponsibleAnswerArg arg, IApprovalResponsibleAnswerDomainService service)
    {
        await ModifyGuards(arg, service);
        ApprovalId = new(arg.ApprovalId);
        ResponsibleAnswerTypeId = new(arg.ResponsibleAnswerTypeId);
        Description = arg.Description;
        ReportDate = arg.ReportDate;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateApprovalResponsibleAnswerArg arg, IApprovalResponsibleAnswerDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyApprovalResponsibleAnswerArg arg, IApprovalResponsibleAnswerDomainService service)
    {

    }
    #endregion
    public ApprovalResponsibleAnswerId Id { get; private set; }
    public ApprovalId ApprovalId { get; private set; }
    public virtual Approval Approval { get; private set; }
    public ResponsibleAnswerTypeId ResponsibleAnswerTypeId { get; private set; }
    public virtual ResponsibleAnswerType ResponsibleAnswerType { get; private set; }
    public string? Description { get; private set; }
    public DateTime ReportDate { get; private set; }
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