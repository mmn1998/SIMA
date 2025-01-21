using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.CandidatedSuppliers;

public class GetAllCandidatedSuppliersByLogesticIdQuery : IQuery<Result<IEnumerable<GetCandidatedSupplierQueryResult>>>
{
    public long Id { get; set; }
}
