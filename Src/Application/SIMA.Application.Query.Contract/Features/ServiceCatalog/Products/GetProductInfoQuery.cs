using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Products
{
    public class GetProductInfoQuery : IQuery<Result<GetProductQueryResult>>
    {
        public long Id { get; set; }
    }
}
