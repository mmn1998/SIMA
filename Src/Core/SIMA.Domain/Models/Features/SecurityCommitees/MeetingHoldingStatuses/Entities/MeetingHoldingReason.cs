using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Entities;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Entities;

public class MeetingHoldingReason
{
    private MeetingHoldingReason() { }
    private MeetingHoldingReason(CreateMeetingHoldingReasonArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Code = arg.Code;
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<MeetingHoldingReason> Create(CreateMeetingHoldingReasonArg arg, IMeetingHoldingReasonDomainService service)
    {
        await CreateGuards(arg, service);
        return new MeetingHoldingReason(arg);
    }
    public async Task Modify(ModifyMeetingHoldingReasonArg arg, IMeetingHoldingReasonDomainService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateMeetingHoldingReasonArg arg, IMeetingHoldingReasonDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyMeetingHoldingReasonArg arg, IMeetingHoldingReasonDomainService service)
    {

    }
    #endregion
    public MeetingHoldingReasonId Id { get; private set; }
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    private List<MeetingReason> _meetingReasons = new();
    public ICollection<MeetingReason> meetingReasons => _meetingReasons;
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
