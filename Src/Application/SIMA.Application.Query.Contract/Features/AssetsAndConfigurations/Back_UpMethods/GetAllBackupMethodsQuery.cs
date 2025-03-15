using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Back_UpMethods;

public class GetAllBackupMethodsQuery : BaseRequest, IQuery<Result<IEnumerable<GetBackupMethodQueryResult>>>
{
}