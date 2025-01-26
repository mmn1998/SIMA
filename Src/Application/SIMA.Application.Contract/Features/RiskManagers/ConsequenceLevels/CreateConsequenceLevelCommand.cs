using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.ConsequenceLevels;

public class CreateConsequenceLevelCommand : ICommand<Result<long>>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public float NumericValue { get; set; }
    public long ConsequenceCategoryId { get; set; }
}