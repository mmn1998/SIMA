using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.ConsequenceLevels;

public class ModifyConsequenceLevelCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public float NumericValue { get; set; }
    public float ValueTitle { get; set; }
    public List<long>? ConsequenceCategoryList { get; set; }
}