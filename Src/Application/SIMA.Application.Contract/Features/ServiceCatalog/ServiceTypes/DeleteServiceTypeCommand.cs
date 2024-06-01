using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServiceTypes;

public class DeleteServiceTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
