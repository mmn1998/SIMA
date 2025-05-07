using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Warehouses;

public class GetWarehouseQuery : IQuery<Result<GetWarehouseQueryResult>>
{
    public long Id { get; set; }
}