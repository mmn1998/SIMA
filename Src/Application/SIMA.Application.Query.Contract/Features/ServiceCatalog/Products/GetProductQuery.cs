using SIMA.Application.Query.Contract.Features.Logistics.GoodsTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Products;

public class GetProductQuery : IQuery<Result<GetProductQueryResult>>
{
    public long Id { get; set; }
}
