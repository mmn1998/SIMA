using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes
{
    public class GetCurrencyTypeQuery : IQuery<Result<GetCurrencyTypeQueryResult>>
    {
        public long Id { get; set; }
    }
}
