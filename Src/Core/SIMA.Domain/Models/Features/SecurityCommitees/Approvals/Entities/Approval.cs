using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;

public class Approval
{
    private Approval() { }
    private Approval(CreateApprovalArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        MeetingId = new(arg.MeetingId);
        IssueId = new(arg.IssueId);
        DueDate = arg.DueDate;
        ResponsibleCompanyId = new(arg.ResponsibleComppanyId);
        SupervisorCompanyId = new CompanyId(arg.SupervisorComppanyId);
        if (arg.ResponsibleDepartmentId.HasValue) ResponsibleDepartmentId = new(arg.ResponsibleDepartmentId.Value);
        if (arg.ResponsibleStaffId.HasValue) ResponsibleStaffId = new(arg.ResponsibleStaffId.Value);
        if (arg.SupervisorDepartmentId.HasValue) SupervisorDepartmentId = new(arg.SupervisorDepartmentId.Value);
        if (arg.SupervisorStaffId.HasValue) SupervisorStaffId = new(arg.SupervisorStaffId.Value);
        IsArchived = arg.IsArchived;
        IsSigned = arg.IsSigned;
        IsPresented = arg.IsPresented;
        if (arg.ArchivedBy.HasValue) ArchivedBy = new(arg.ArchivedBy.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Approval> Create(CreateApprovalArg arg, IApprovalDomainService service)
    {
        await CreateGuards(arg, service);
        return new Approval(arg);
    }
    public async Task Modify(ModifyApprovalArg arg, IApprovalDomainService service)
    {
        await ModifyGuards(arg, service);
        MeetingId = new(arg.MeetingId);
        IssueId = new(arg.IssueId);
        DueDate = arg.DueDate;
        ResponsibleCompanyId = new(arg.ResponsibleComppanyId);
        SupervisorCompanyId = new CompanyId(arg.SupervisorComppanyId);
        if (arg.ResponsibleDepartmentId.HasValue) ResponsibleDepartmentId = new(arg.ResponsibleDepartmentId.Value);
        if (arg.ResponsibleStaffId.HasValue) ResponsibleStaffId = new(arg.ResponsibleStaffId.Value);
        if (arg.SupervisorDepartmentId.HasValue) SupervisorDepartmentId = new(arg.SupervisorDepartmentId.Value);
        if (arg.SupervisorStaffId.HasValue) SupervisorStaffId = new(arg.SupervisorStaffId.Value);
        IsArchived = arg.IsArchived;
        IsSigned = arg.IsSigned;
        IsPresented = arg.IsPresented;
        if (arg.ArchivedBy.HasValue) ArchivedBy = new(arg.ArchivedBy.Value);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateApprovalArg arg, IApprovalDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyApprovalArg arg, IApprovalDomainService service)
    {

    }
    #endregion
    public ApprovalId Id { get; private set; }
    public MeetingId MeetingId { get; private set; }
    public IssueId IssueId { get; private set; }
    public DateTime DueDate { get; private set; }
    public CompanyId ResponsibleCompanyId { get; private set; }
    public DepartmentId? ResponsibleDepartmentId { get; private set; }
    public StaffId? ResponsibleStaffId { get; private set; }
    public CompanyId SupervisorCompanyId { get; private set; }
    public DepartmentId? SupervisorDepartmentId { get; private set; }
    public StaffId? SupervisorStaffId { get; private set; }
    public string? IsPresented { get; private set; }
    public string? IsSigned { get; private set; }
    public string? IsArchived { get; private set; }
    public UserId? ArchivedBy { get; private set; }
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
