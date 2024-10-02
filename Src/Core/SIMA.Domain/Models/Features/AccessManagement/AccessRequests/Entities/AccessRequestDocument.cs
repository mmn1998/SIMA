using SIMA.Domain.Models.Features.AccessManagement.AccessRequests.Args;
using SIMA.Domain.Models.Features.AccessManagement.AccessRequests.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AccessManagement.AccessRequests.Entities;

public class AccessRequestDocument : Entity
{
    private AccessRequestDocument() { }
    private AccessRequestDocument(CreateAccessRequestDocumentArg arg)
    {
        Id = new(arg.Id);
        DocumentId = new(arg.DocumentId);
        AccessRequestId = new(arg.AccessRequestId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static AccessRequestDocument Create(CreateAccessRequestDocumentArg arg)
    {
        return new AccessRequestDocument(arg);
    }
    public AccessRequestDocumentId Id { get; private set; }
    
    public DocumentId DocumentId { get; private set; }
    public virtual Document Document { get; private set; }
    public AccessRequestId AccessRequestId { get; private set; }
    public virtual AccessRequest AccessRequest { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<AccessRequestDocument> _accessRequestDocuments = new();
    public ICollection<AccessRequestDocument> AccessRequestDocuments => _accessRequestDocuments;
}
