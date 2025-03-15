using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetProductList: IQuery<Result<List<GetProductListResult>>>
{
    public string? Type { get; set; }
    public long? Id { get; set; }
}