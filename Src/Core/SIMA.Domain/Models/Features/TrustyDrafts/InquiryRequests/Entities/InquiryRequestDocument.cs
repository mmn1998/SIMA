using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;

public class InquiryRequestDocument : Entity
{
    private InquiryRequestDocument()
    {

    }
    private InquiryRequestDocument(CreateInquiryRequestDocumentArg arg)
    {
        Id = new(arg.Id);
        DocumentId = new(arg.DocumentId);
        InquiryRequestId = new(arg.InquiryRequestId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static InquiryRequestDocument Create(CreateInquiryRequestDocumentArg arg)
    {
        return new InquiryRequestDocument(arg);
    }
    public InquiryRequestDocumentId Id { get; private set; }
    public InquiryRequestId InquiryRequestId { get; private set; }
    public virtual InquiryRequest InquiryRequest { get; private set; }
    public DocumentId DocumentId { get; private set; }
    public virtual Document Document { get; private set; }
    public long ActiveStatusId { get; private set; }
    public long? CreatedBy { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}