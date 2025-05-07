using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseStatuses;

public class GetLicenseStatusQuery : IQuery<Result<GetLicenseStatusQueryResult>>
{
    public long Id { get; set; }
}