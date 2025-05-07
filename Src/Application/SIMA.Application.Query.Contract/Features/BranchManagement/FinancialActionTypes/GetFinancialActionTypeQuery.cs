using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.FinancialActionTypes
{
    public class GetFinancialActionTypeQuery : IQuery<Result<GetFinancialActionTypeQueryResult>>
    {
        public long Id { get; set; }
    }
}
