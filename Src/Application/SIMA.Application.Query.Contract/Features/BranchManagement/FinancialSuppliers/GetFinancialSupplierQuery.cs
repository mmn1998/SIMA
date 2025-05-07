using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.FinancialSuppliers
{
    public class GetFinancialSupplierQuery : IQuery<Result<GetFinancialSupplierQueryResult>>
    {
        public long Id { get; set; }
    }
}
