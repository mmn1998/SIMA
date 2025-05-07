using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataCenters;

public class GetAllDataCentersQuery : BaseRequest, IQuery<Result<IEnumerable<GetDataCenterQueryResult>>>
{
}