using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.AccountTypes;

public class GetAccountTypeQuery : IQuery<Result<GetAccountTypeQueryResult>>
{
    public long Id { get; set; }
}