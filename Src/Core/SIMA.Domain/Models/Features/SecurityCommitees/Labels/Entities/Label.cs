using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Interface;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.ValueObject;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;

public class Label : Entity
{
    private Label() { }
    private Label(CreateLabelArg arg)
    {
        Id = new LabelId(arg.Id);
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Label> Create(CreateLabelArg arg)
    {
        //await CreateGuards(arg);
        return new Label(arg);
    }
    public async Task Modify(ModifyLabelArg arg, ILabelDomainService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateLabelArg arg, ILabelDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyLabelArg arg, ILabelDomainService service)
    {

    }
    #endregion
    public LabelId Id { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<MeetingLabel> _meetingLabels = new();
    public List<MeetingLabel> MeetingLabels => _meetingLabels;
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
