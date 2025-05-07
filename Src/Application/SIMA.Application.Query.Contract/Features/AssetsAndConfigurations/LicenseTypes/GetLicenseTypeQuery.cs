using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseTypes;

public class GetLicenseTypeQuery : IQuery<Result<GetLicenseTypeQueryResult>>
{
    public long Id { get; set; }
}