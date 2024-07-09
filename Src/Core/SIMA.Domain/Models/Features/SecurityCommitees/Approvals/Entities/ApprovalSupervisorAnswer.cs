using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.SupervisorAnswerTypes.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.SupervisorAnswerTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;

public class ApprovalSupervisorAnswer
{
    private ApprovalSupervisorAnswer() { }
    private ApprovalSupervisorAnswer(CreateApprovalSupervisorAnswerArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        ApprovalId = new(arg.ApprovalId);
        SupervisorAnswerTypeId = new(arg.SupervisorAnswerTypeId);
        Description = arg.Description;
        ReportDate = arg.ReportDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ApprovalSupervisorAnswer> Create(CreateApprovalSupervisorAnswerArg arg, IApprovalSupervisorAnswerDomainService service)
    {
        await CreateGuards(arg, service);
        return new ApprovalSupervisorAnswer(arg);
    }
    public async Task Modify(ModifyApprovalSupervisorAnswerArg arg, IApprovalSupervisorAnswerDomainService service)
    {
        await ModifyGuards(arg, service);
        ApprovalId = new(arg.ApprovalId);
        SupervisorAnswerTypeId = new(arg.SupervisorAnswerTypeId);
        Description = arg.Description;
        ReportDate = arg.ReportDate;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateApprovalSupervisorAnswerArg arg, IApprovalSupervisorAnswerDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyApprovalSupervisorAnswerArg arg, IApprovalSupervisorAnswerDomainService service)
    {

    }
    #endregion

    public ApprovalSupervisorAnswerId Id { get; private set; }
    public ApprovalId ApprovalId { get; private set; }
    public virtual Approval Approval { get; private set; }
    public SupervisorAnswerTypeId SupervisorAnswerTypeId { get; private set; }
    public virtual SupervisorAnswerType SupervisorAnswerType { get; private set; }
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
