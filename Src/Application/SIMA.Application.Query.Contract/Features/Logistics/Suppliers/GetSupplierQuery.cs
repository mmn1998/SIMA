using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.Suppliers;

public class GetSupplierQuery : IQuery<Result<GetSupplierQueryResult>>
{
    public long Id { get; set; }
}