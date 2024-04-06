using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Entities;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Entities;

public class MeetingHoldingStatus
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

    }
    private async Task ModifyGuards(ModifyMeetingHoldingStatusArg arg, IMeetingHoldingStatusDomainService service)
    {

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