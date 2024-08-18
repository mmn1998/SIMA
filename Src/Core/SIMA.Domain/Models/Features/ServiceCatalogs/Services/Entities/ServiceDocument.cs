using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Core.Entities;
using System.Xml.Linq;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class ServiceDocument : Entity
{
    private ServiceDocument()
    {
    }
    private ServiceDocument(CreateServiceDocumentArg arg)
    {
        Id = new ServiceDocumentId(arg.Id);
        ServiceId = new ServiceId(arg.ServiceId);
        DocumentId = new DocumentId(arg.DocumentId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceDocument> Create(CreateServiceDocumentArg arg)
    {
        return new ServiceDocument(arg);
    }
    public ServiceDocumentId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Document Document { get; private set; }
    public DocumentId DocumentId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
