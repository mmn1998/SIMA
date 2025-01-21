using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.BusinessContinuityStrategies;

public class ModifyBusinessContinuityStrategyCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long StrategyTypeId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ExpireDate { get; set; }
    public string? ReviewDate { get; set; }
    public List<string>? BusinessContinuityStrategyObjectiveList { get; set; }
    public List<string>? BusinessContinuityStrategySolutionList { get; set; }
    public List<long>? BusinessContinuityStrategyDocumentList { get; set; }
    //public List<CreateBusinessContinuityStrategyResponsibleCommand>? BusinessContinuityStrategyResponsibleList { get; set; }
}