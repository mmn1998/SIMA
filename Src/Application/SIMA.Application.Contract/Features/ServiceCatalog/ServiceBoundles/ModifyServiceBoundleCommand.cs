using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServiceBoundles;

public class ModifyServiceBoundleCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long ServiceCategoryId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}
