using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes
{
    public class GetAllCurrencyTypesQuery : IQuery<Result<List<GetCurrencyTypeQueryResult>>>
    {
        public BaseRequest Request { get; set; } = new();
    }
}
