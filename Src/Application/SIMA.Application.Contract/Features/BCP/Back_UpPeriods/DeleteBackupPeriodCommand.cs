using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.Back_UpPeriods;

public class DeleteBackupPeriodCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}