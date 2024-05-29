using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriority.Args;

public class CreateServicePriorityArg
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Ordering { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
