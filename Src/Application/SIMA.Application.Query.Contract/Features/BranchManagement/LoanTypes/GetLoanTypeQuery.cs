using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.LoanTypes;

public class GetLoanTypeQuery : IQuery<Result<GetLoanTypeQueryResult>>
{
    public long Id { get; set; }
}