using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Entities;

public class MeetingHoldingReason : Entity
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
    private async Task ModifyGuards(ModifyMeetingHoldingReasonArg arg, IMeetingHoldingReasonDomainService service)
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
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
