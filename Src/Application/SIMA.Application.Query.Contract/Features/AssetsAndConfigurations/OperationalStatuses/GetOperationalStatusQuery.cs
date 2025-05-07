using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.OperationalStatuses;

public class GetOperationalStatusQuery: IQuery<Result<GetOperationalStatusQueryResult>>
{
    public long Id { get; set; }
}