using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Entities;

public class MeetingSchedule
{
    private MeetingSchedule() { }
    private MeetingSchedule(CreateMeetingScheduleArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        MeetingId = new(arg.MeetingId);
        MeetingDateTime = arg.MeetingDateTime;
        Location = arg.Location;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<MeetingSchedule> Create(CreateMeetingScheduleArg arg, IMeetingScheduleDomainService service)
    {
        await CreateGuards(arg, service);
        return new MeetingSchedule(arg);
    }
    public async Task Modify(ModifyMeetingScheduleArg arg, IMeetingScheduleDomainService service)
    {
        await ModifyGuards(arg, service);
        MeetingId = new(arg.MeetingId);
        MeetingDateTime = arg.MeetingDateTime;
        Location = arg.Location;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateMeetingScheduleArg arg, IMeetingScheduleDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyMeetingScheduleArg arg, IMeetingScheduleDomainService service)
    {

    }
    #endregion
    public MeetingScheduleId Id { get; private set; }
    public MeetingId MeetingId { get; private set; }
    public virtual Meeting Meeting { get; private set; }
    public DateTime MeetingDateTime { get; private set; }
    public string? Location { get; private set; }
    /// <summary>
    /// TODO MeetingHoldingStatusId
    /// </summary>
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public virtual ICollection<Invitees> Inviteeses { get; private set; }

    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
