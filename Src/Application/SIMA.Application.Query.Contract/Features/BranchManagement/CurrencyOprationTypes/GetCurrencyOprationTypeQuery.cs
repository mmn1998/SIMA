using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyOprationTypes
{
    public class GetCurrencyOprationTypeQuery : IQuery<Result<GetCurrencyOprationTypeQueryResult>>
    {
        public long Id { get; set; }
    }
}
