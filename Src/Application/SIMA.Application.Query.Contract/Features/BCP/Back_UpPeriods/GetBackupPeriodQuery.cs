using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.Back_UpPeriods;

public class GetBackupPeriodQuery : IQuery<Result<GetBackupPeriodQueryResult>>
{
    public long Id { get; set; }
}