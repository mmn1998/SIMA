using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.CustomerTypes;

public class GetCustomerTypeQuery : IQuery<Result<GetCustomerTypeQueryResult>>
{
    public long Id { get; set; }
}