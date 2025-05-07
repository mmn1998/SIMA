using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Products;

public class GetProductByCodeQuery: IQuery<Result<GetProductQueryResult>>
{
    public string Code { get; set; }
}
