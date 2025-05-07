using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.SupplierRanks;

public class GetAllSupplierRanksQuery : BaseRequest, IQuery<Result<IEnumerable<GetSupplierRankQueryResult>>>
{
}