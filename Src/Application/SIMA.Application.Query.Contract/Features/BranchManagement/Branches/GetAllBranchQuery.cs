using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using System.Text.Json.Serialization;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.Branches;

public class GetAllBranchQuery : BaseRequest, IQuery<Result<IEnumerable<GetBranchQueryResult>>>
{
}