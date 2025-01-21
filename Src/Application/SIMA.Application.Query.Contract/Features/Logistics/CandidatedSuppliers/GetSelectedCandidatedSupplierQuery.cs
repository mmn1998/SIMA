using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.CandidatedSuppliers
{
    public class GetSelectedCandidatedSupplierQuery : IQuery<Result<IEnumerable<GetCandidatedSupplierQueryResult>>>
    {
        public long LogisticsSupplyId { get; set; }
    }
}
