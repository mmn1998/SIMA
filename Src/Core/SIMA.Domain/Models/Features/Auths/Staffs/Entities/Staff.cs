using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Args;
using SIMA.Domain.Models.Features.Auths.Staffs.Exceptions;
using SIMA.Domain.Models.Features.Auths.Staffs.Interfaces;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.Staffs.Entities;

public class Staff : Entity
{
    private Staff() { }
    private Staff(CreateStaffArg arg)
    {
        Id = new StaffId(IdHelper.GenerateUniqueId());
        if (arg.ProfileId.HasValue) ProfileId = new ProfileId(arg.ProfileId.Value);
        if (arg.PositionId.HasValue) PositionId = new PositionId(arg.PositionId.Value);
        if (arg.ManagerId.HasValue) ManagerId = new ProfileId(arg.ManagerId.Value);
        StaffNumber = arg.StaffNumber;
        ActiveFrom = arg.ActiveFrom;
        ActiveTo = arg.ActiveTo;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public async Task Modify(ModifyStaffArg arg, IStaffService service)
    {
        await ModifyGuards(arg, service);
        if (arg.ProfileId.HasValue) ProfileId = new ProfileId(arg.ProfileId.Value);
        if (arg.PositionId.HasValue) PositionId = new PositionId(arg.PositionId.Value);
        if (arg.ManagerId.HasValue) ManagerId = new ProfileId(arg.ManagerId.Value);
        StaffNumber = arg.StaffNumber;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    //public static async Task<Staff> New(IStaffService service, CreateStaffArg arg)
    //{
    //    var validator = new StaffValidator(service);
    //    await validator.ValidateAndThrowAsync(arg);
    //    return new Staff(arg);
    //}

    public static async Task<Staff> Create(CreateStaffArg arg, IStaffService service)
    {
        await CreateGuards(arg, service);
        return new Staff(arg);
    }

    #region Guards
    private static async Task CreateGuards(CreateStaffArg arg, IStaffService service)
    {
        arg.NullCheck();
        arg.ProfileId.NullCheck();
        arg.ManagerId.NullCheck();
        arg.PositionId.NullCheck();

        if (arg.PositionId.HasValue && arg.ProfileId.HasValue)
        {
            if (!await service.IsPositionDuplicated(arg.PositionId.Value, arg.ProfileId.Value)) throw SimaResultException.PersonHasDuplicatePositionError;
            if (!await service.IsStaffSatisfied(arg.ProfileId.Value, arg.PositionId.Value)) throw StaffExceptions.StaffNotSatisfiedException;
        }
    }
    private async Task ModifyGuards(ModifyStaffArg arg, IStaffService service)
    {
        arg.NullCheck();
        arg.ProfileId.NullCheck();
        arg.ManagerId.NullCheck();
        arg.PositionId.NullCheck();

        if (arg.PositionId.HasValue && arg.ProfileId.HasValue)
        {
            if (!await service.IsPositionDuplicated(arg.PositionId.Value, arg.ProfileId.Value)) throw SimaResultException.PersonHasDuplicatePositionError;
            if (!await service.IsStaffSatisfied(arg.ProfileId.Value, arg.PositionId.Value)) throw StaffExceptions.StaffNotSatisfiedException;
        }
    }
    #endregion
    public StaffId Id { get; private set; }

    public ProfileId? ProfileId { get; private set; }

    public ProfileId? ManagerId { get; private set; }

    public PositionId? PositionId { get; private set; }

    public string? StaffNumber { get; private set; }

    public DateOnly? ActiveFrom { get; private set; }

    public DateOnly? ActiveTo { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual Profile? Manager { get; private set; }

    public virtual Position? Position { get; private set; }

    public virtual Profile? Profile { get; private set; }
    public virtual ICollection<Branch> ChiefBranches { get; private set; }
    public virtual ICollection<Branch> DeputyBranches { get; private set; }

    private List<Approval> _responsibleApprovals => new();
    public ICollection<Approval> ResponsibleApprovals => _responsibleApprovals;
    private List<Approval> _supervisorApprovals => new();
    public ICollection<Approval> SupervisorApprovals => _supervisorApprovals;
    private List<Invitees> _invitees => new();
    public ICollection<Invitees> Invitees => _invitees;
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
