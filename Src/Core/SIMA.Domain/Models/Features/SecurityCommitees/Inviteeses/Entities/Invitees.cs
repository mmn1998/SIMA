using SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Entities;

public class Invitees
{
    private Invitees() { }
    private Invitees(CreateInviteesArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        MeetingScheduleId = new(arg.MeetingScheduleId);
        CompanyId = new(arg.ComppanyId);
        if (arg.DepartmentId.HasValue) DepartmentId = new(arg.DepartmentId.Value);
        if (arg.StaffId.HasValue) StaffId = new(arg.StaffId.Value);
        IsCompanyRepresentative = arg.IsCompanyRepresentative;
        IsPresented = arg.IsPresented;
        IsSigned = arg.IsSigned;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Invitees> Create(CreateInviteesArg arg, IInviteesDomaiService service)
    {
        await CreateGuards(arg, service);
        return new Invitees(arg);
    }
    public async Task Modify(ModifyInviteesArg arg, IInviteesDomaiService service)
    {
        await ModifyGuards(arg, service);
        MeetingScheduleId = new(arg.MeetingScheduleId);
        CompanyId = new(arg.ComppanyId);
        if (arg.DepartmentId.HasValue) DepartmentId = new(arg.DepartmentId.Value);
        if (arg.StaffId.HasValue) StaffId = new(arg.StaffId.Value);
        IsCompanyRepresentative = arg.IsCompanyRepresentative;
        IsPresented = arg.IsPresented;
        IsSigned = arg.IsSigned;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateInviteesArg arg, IInviteesDomaiService service)
    {

    }
    private async Task ModifyGuards(ModifyInviteesArg arg, IInviteesDomaiService service)
    {

    }
    #endregion
    public InviteesId Id { get; private set; }
    public MeetingScheduleId MeetingScheduleId { get; private set; }
    public CompanyId CompanyId { get; private set; }
    public DepartmentId? DepartmentId { get; private set; }
    public StaffId? StaffId { get; private set; }
    public string? IsCompanyRepresentative { get; private set; }
    public string? IsPresented { get; private set; }
    public string? IsSigned { get; private set; }
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
