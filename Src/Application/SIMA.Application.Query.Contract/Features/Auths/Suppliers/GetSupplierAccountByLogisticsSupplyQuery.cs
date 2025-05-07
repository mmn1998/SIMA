using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Suppliers
{
    public class GetSupplierAccountByLogisticsSupplyQuery :  IQuery<Result<IEnumerable<GetSupplierAccountByLogisticsSupplyQueryResult>>>
    {
        public long LogisticsSupplyId { get; set; }
    }
}
