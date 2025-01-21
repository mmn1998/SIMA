using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.LoanTypes;

public class GetAllLoanTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetLoanTypeQueryResult>>>
{
}