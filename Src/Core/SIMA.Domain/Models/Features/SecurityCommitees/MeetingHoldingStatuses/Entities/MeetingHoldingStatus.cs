using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Entities;

public class MeetingHoldingStatus : Entity
{
    private MeetingHoldingStatus() { }
    private MeetingHoldingStatus(CreateMeetingHoldingStatusArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Code = arg.Code;
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<MeetingHoldingStatus> Create(CreateMeetingHoldingStatusArg arg, IMeetingHoldingStatusDomainService service)
    {
        await CreateGuards(arg, service);
        return new MeetingHoldingStatus(arg);
    }
    public async Task Modify(ModifyMeetingHoldingStatusArg arg, IMeetingHoldingStatusDomainService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateMeetingHoldingStatusArg arg, IMeetingHoldingStatusDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await service.IsCodeUnique(arg.Code, 0))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyMeetingHoldingStatusArg arg, IMeetingHoldingStatusDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await service.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    #endregion
    public MeetingHoldingStatusId Id { get; private set; }
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<MeetingSchedule> _meetingSchedules = new();
    public ICollection<MeetingSchedule> MeetingSchedules => _meetingSchedules;
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}