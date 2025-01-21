using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServiceStatuses;

public class DeleteServiceStatusCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
