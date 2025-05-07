using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.ServiceCatalog.Products;

public class DeleteProductCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}