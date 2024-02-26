using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.Branches;

public class GetAllBranchQuery : IQuery<Result<List<GetBranchQueryResult>>>
{
    public BaseRequest Request { get; set; } = new();
}
