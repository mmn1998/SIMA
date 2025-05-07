using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.CandidatedSuppliers;

public class GetAllCandidatedSuppliersQuery : BaseRequest, IQuery<Result<IEnumerable<GetCandidatedSupplierQueryResult>>>
{
}
