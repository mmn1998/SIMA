using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServiceBoundles;

public class DeleteServiceBoundleCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
