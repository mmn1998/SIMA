using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.AffectedHistories;

public class DeleteAffectedHistoryCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}