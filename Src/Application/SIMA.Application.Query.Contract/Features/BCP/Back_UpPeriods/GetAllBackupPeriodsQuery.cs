using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.Back_UpPeriods;

public class GetAllBackupPeriodsQuery : BaseRequest, IQuery<Result<IEnumerable<GetBackupPeriodQueryResult>>>
{
}