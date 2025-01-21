using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseTypes;

public class GetAllLicenseTypeQuery : BaseRequest, IQuery<Result<IEnumerable<GetLicenseTypeQueryResult>>>
{
}