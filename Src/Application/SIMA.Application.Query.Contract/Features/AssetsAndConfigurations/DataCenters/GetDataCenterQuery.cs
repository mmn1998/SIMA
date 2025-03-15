using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataCenters;

public class GetDataCenterQuery : IQuery<Result<GetDataCenterQueryResult>>
{
    public long Id { get; set; }
}