using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;

public class MeetingReason
{
    private MeetingReason() { }
    private MeetingReason(CreateMeetingReasonArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        MeetingId = new(arg.MeetingId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<MeetingReason> Create(CreateMeetingReasonArg arg, IMeetingReasonDomainService service)
    {
        await CreateGuards(arg, service);
        return new MeetingReason(arg);
    }
    public async Task Modify(ModifyMeetingReasonArg arg, IMeetingReasonDomainService service)
    {
        await ModifyGuards(arg, service);
        MeetingId = new(arg.MeetingId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateMeetingReasonArg arg, IMeetingReasonDomainService service)
    {
        
    }
    private async Task ModifyGuards(ModifyMeetingReasonArg arg, IMeetingReasonDomainService service)
    {
        
    }
    #endregion
    public MeetingReasonId Id { get; private set; }
    /// <summary>
    /// TODO : MeetingHoldingReasonId
    /// </summary>
    public MeetingId MeetingId { get; private set; }
    public virtual Meeting Meeting { get; private set; }
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
