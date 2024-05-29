using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Args;

public class CreateServiceBoundleArg
{
    public long ServiceCategoryId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}
