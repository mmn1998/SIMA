using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyOprationTypes
{
    public class GetAllCurrencyOprationTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetCurrencyOprationTypeQueryResult>>>
    {
    }
}
