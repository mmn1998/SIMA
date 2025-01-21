using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.FinancialActionTypes
{
    public class GetAllFinancialActionTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetFinancialActionTypeQueryResult>>>
    {
    }
}
