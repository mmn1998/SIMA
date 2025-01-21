using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.AccountTypes;

public class GetAllAccountTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetAccountTypeQueryResult>>>
{
}