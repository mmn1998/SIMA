using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskLevelCobits;

public class DeleteRiskLevelCobitCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}