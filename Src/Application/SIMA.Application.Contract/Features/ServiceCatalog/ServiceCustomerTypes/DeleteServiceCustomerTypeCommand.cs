using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServiceCustomerTypes;

public class DeleteServiceCustomerTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}