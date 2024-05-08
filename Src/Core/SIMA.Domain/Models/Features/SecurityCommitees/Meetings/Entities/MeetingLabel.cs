using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.ValueObject;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;

public class MeetingLabel
{
    private MeetingLabel() { }
    private MeetingLabel(CreateMeetingLabelArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        MeetingId = new(arg.MeetingId);
        LabelId = new(arg.LabelId);
        ActiveStatusId = (long)ActiveStatusEnum.Active;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    private MeetingLabel(long meetingId, long labelId)
    {
        Id = new(IdHelper.GenerateUniqueId());
        MeetingId = new(meetingId);
        LabelId = new(labelId);
        ActiveStatusId = (long)ActiveStatusEnum.Active;
        CreatedAt = DateTime.Now;
        
    }
    public static MeetingLabel Create(CreateMeetingLabelArg arg)
    {
        //await CreateGuards(arg, service);
        return new MeetingLabel(arg);
    }
    public static MeetingLabel Create(long meetingId, long labelId)
    {
        return new MeetingLabel(meetingId , labelId);
    }
    public async Task Modify(ModifyMeetingLabelArg arg, IMeetingLabelDomainService service)
    {
        await ModifyGuards(arg, service);
        MeetingId = new(arg.MeetingId);
        LabelId = new(arg.LabelId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    #region Guards
    private static async Task CreateGuards(CreateMeetingLabelArg arg, IMeetingLabelDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyMeetingLabelArg arg, IMeetingLabelDomainService service)
    {

    }
    #endregion
    public MeetingLabelId Id { get; private set; }
    public MeetingId MeetingId { get; private set; }
    public virtual Meeting Meeting { get; private set; }
    public LabelId LabelId { get; private set; }
    public virtual Label Label { get; private set; }
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
