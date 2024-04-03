using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;

public class MeetingDocument
{
    private MeetingDocument() { }
    private MeetingDocument(CreateMeetingDocumentArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        MeetingId = new(arg.MeetingId);
        DocumentId = new(arg.DocumentId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<MeetingDocument> Create(CreateMeetingDocumentArg arg, IMeetingDocumentDomainService service)
    {
        await CreateGuards(arg, service);
        return new MeetingDocument(arg);
    }
    public async Task Modify(ModifyMeetingDocumentArg arg, IMeetingDocumentDomainService service)
    {
        await ModifyGuards(arg, service);
        MeetingId = new(arg.MeetingId);
        DocumentId = new(arg.DocumentId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateMeetingDocumentArg arg, IMeetingDocumentDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyMeetingDocumentArg arg, IMeetingDocumentDomainService service)
    {

    }
    #endregion
    public MeetingDocumentId Id { get; private set; }
    public MeetingId MeetingId { get; private set; }
    public virtual Meeting Meeting { get; private set; }
    public DocumentId DocumentId { get; private set; }
    public virtual Document Document { get; private set; }
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
