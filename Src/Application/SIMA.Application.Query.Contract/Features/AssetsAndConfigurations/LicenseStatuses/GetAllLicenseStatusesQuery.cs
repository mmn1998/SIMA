using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseStatuses;

public class GetAllLicenseStatusesQuery : BaseRequest, IQuery<Result<IEnumerable<GetLicenseStatusQueryResult>>>
{
}