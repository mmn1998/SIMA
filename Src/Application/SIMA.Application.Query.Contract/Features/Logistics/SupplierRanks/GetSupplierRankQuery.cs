using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.SupplierRanks;

public class GetSupplierRankQuery : IQuery<Result<GetSupplierRankQueryResult>>
{
    public long Id { get; set; }
}