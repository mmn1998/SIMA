using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.Customers;

public class GetCustomerQuery : IQuery<Result<GetCustomerQueryResult>>
{
    public long Id { get; set; }
}