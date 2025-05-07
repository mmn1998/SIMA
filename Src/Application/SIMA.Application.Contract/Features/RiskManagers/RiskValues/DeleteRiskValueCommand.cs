using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskValues;

public class DeleteRiskValueCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}