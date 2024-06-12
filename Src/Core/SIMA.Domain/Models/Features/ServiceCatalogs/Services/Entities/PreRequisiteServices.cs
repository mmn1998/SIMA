using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class PreRequisiteServices : Entity
{
    private PreRequisiteServices()
    {

    }
    private PreRequisiteServices(CreatePreRequisiteServicesArg arg)
    {
        Id = new PreRequisiteServicesId(arg.Id);

        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<PreRequisiteServices> Create(CreatePreRequisiteServicesArg arg)
    {
        return new PreRequisiteServices(arg);
    }
    public PreRequisiteServicesId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public ServiceId PreRequiredServiceId { get; private set; }
    public virtual Service PreRequiredService { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

}
